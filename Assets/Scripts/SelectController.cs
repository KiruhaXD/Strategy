using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class SelectController : MonoBehaviour
{
    // тут мы создаем область объектов, то есть их выделение или выбора игровых объектов

    public GameObject cube; // сам куб
    public List<GameObject> cars; 
    public LayerMask layer, layerMask; // место куда падает виртуальный луч
    private Camera _camera; // сама камера
    private GameObject cubeSelection; // и удаление куба при отпускании клавиши мыши
    private RaycastHit hit;

    private void Awake()
    {   
        _camera = GetComponent<Camera>();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            // очищаем список объектов при повторной выборке
            cars.Clear();

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            
            if (Physics.Raycast(ray, out hit, 1000f, layer))
                                // тут мы создаем куб
                cubeSelection = Instantiate(cube, new Vector3(hit.point.x, 1, hit.point.z), Quaternion.identity);
            
        }

        if (cubeSelection)
        {

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            // hitDrag - перетаскивание
            if (Physics.Raycast(ray, out RaycastHit hitDrag, 1000f, layer))
            {
                // тут мы делаем исправление ошибки с "Box Collider"
                float xScale = (hit.point.x - hitDrag.point.x) * -1;
                float zScale = (hit.point.z - hitDrag.point.z);

                if (xScale < 0.0f && zScale < 0.0f)
                    // переворачиваем куб и увеличиваем его в противоположную сторону
                    cubeSelection.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));

                // тут мы создаем доп, условия для того чтобы наша область дял выбора объектов создавалась правильно и ее
                // можно было разворачивать в любые стороны
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
            RaycastHit[] hits = Physics.BoxCastAll( // BoxCastAll отслеживает различные Box-коллайдеры
                cubeSelection.transform.position,   // позиция нашего куба
                cubeSelection.transform.localScale, // размер нашего куба
                Vector3.up,                         // в каком векторе мы будем выбирать все элементы
                Quaternion.identity,                // вектор с тремя нулями, так как вращение нам тут не нужно
                0,                                  // максимальное расстояние
                layerMask);                         // какие элементы, с каким слоем мы будем выбирать

            foreach (var elem in hits) // тут мы перебираем все что находится в массиве hits
            {
                cars.Add(elem.transform.gameObject);
            }

            // тут мы при отпускании клавиши мыши будем удалять созданный нами куб
            Destroy(cubeSelection);
        }

        
                

        

      
    }

}
