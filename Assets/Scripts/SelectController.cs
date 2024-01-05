using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class SelectController : MonoBehaviour
{
    // ��� �� ������� ������� ��������, �� ���� �� ��������� ��� ������ ������� ��������

    public GameObject cube; // ��� ���
    public List<GameObject> cars; 
    public LayerMask layer, layerMask; // ����� ���� ������ ����������� ���
    private Camera _camera; // ���� ������
    private GameObject cubeSelection; // � �������� ���� ��� ���������� ������� ����
    private RaycastHit hit;

    private void Awake()
    {   
        _camera = GetComponent<Camera>();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            // ������� ������ �������� ��� ��������� �������
            cars.Clear();

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            
            if (Physics.Raycast(ray, out hit, 1000f, layer))
                                // ��� �� ������� ���
                cubeSelection = Instantiate(cube, new Vector3(hit.point.x, 1, hit.point.z), Quaternion.identity);
            
        }

        if (cubeSelection)
        {

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            // hitDrag - ��������������
            if (Physics.Raycast(ray, out RaycastHit hitDrag, 1000f, layer))
            {
                // ��� �� ������ ����������� ������ � "Box Collider"
                float xScale = (hit.point.x - hitDrag.point.x) * -1;
                float zScale = (hit.point.z - hitDrag.point.z);

                if (xScale < 0.0f && zScale < 0.0f)
                    // �������������� ��� � ����������� ��� � ��������������� �������
                    cubeSelection.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));

                // ��� �� ������� ���, ������� ��� ���� ����� ���� ������� ��� ������ �������� ����������� ��������� � ��
                // ����� ���� ������������� � ����� �������
                else if (xScale < 0.0f)
                    cubeSelection.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180));

                else if (zScale < 0.0f)
                    cubeSelection.transform.localRotation = Quaternion.Euler(new Vector3(180, 0, 0));

                else
                    cubeSelection.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                
                cubeSelection.transform.localScale = new Vector3(Mathf.Abs(xScale), 1, Mathf.Abs(zScale));
            }
        }


            if (Input.GetMouseButtonUp(0) && cubeSelection) 
        {
            RaycastHit[] hits = Physics.BoxCastAll( // BoxCastAll ����������� ��������� Box-����������
                cubeSelection.transform.position,   // ������� ������ ����
                cubeSelection.transform.localScale, // ������ ������ ����
                Vector3.up,                         // � ����� ������� �� ����� �������� ��� ��������
                Quaternion.identity,                // ������ � ����� ������, ��� ��� �������� ��� ��� �� �����
                0,                                  // ������������ ����������
                layerMask);                         // ����� ��������, � ����� ����� �� ����� ��������

            foreach (var elem in hits) // ��� �� ���������� ��� ��� ��������� � ������� hits
            {
                cars.Add(elem.transform.gameObject);
            }

            // ��� �� ��� ���������� ������� ���� ����� ������� ��������� ���� ���
            Destroy(cubeSelection);
        }

        
                

        

      
    }

}
