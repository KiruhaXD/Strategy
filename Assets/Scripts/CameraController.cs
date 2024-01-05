using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // �������� �������� ������      // ���������   // �������� ����������� � ��������� ������
    public float rotateSpeed = 10.0f, speed = 10.0f, zoomSpeed = 10.0f;
    

    private float _mult = 1f; // �������� �� ���������

    // ����� ��� �������� ������
    private void Update()
    {

        // ������� ���������� ��� ���� ����� ���� ������������ �� ����� �������� �� �����-������ �������
        // ��������� � ��������� ������, �� ������ ����� ������ �� �����
        float rotate = 0f;

        // ������������ ������ �����, ����, �����, ������
        float hor = Input.GetAxis("Horizontal"); // ������ ���������� �������� ��������� ����������� ����� ������� ��� 
                                                 // ��������� ������ � ����� � ������� A D
        float ver = Input.GetAxis("Vertical"); // Vertical ����� ��������� ����������� ��������� ����� � ���� � ������� W S 

        // GetKey ���������� ��� ����������� �������  �� ������ �������
        if (Input.GetKey(KeyCode.Q)) // ������� ������ �����
            rotate = -1f;
        else if (Input.GetKey(KeyCode.E)) // ������� ������ ������
            rotate = 1f;

        // ���������, ���� ������������ ����� ����� ���� �� ��� ����� ����������, ����� ����� ��������� � ������� ���������
        _mult = Input.GetKey(KeyCode.LeftShift) ? 2f : 1f;

        // up - ���������� ������ ���������� y, ����� �������� �� �������� �������� ������
        // Time.deltaTime - ��������� �������� ��������� � ������-���� ��������
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime * rotate * _mult, Space.World);
        // ��� �������� ������ ������������ ����������� ��������� �� ��������� �������� Space.World
        transform.Translate(new Vector3(hor, 0, ver) * Time.deltaTime * _mult * speed, Space.Self);
        //����� Translate ��������� ����������� ������
        // Self �� �������
        // ��� ��������� ���������
        // ��� ���� ����� ������
        // ��������� � �� �������
        // ���� �� ��������

        // ��� �� ������ ����������� � ��������� ������ c ������� ������� ����
        transform.position += transform.up * zoomSpeed * Time.deltaTime * Input.GetAxis("Mouse ScrollWheel");
        // up ��� �������� �� Y-�� 

        //����������� ������� ������
        transform.position = new Vector3(
            transform.position.x,
            Mathf.Clamp(transform.position.y, -20f, 30f),
            transform.position.z);
    }
}
