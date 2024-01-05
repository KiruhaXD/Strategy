using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlaceObjects : MonoBehaviour
{
    public LayerMask layer;

    public float rotateSpeed = 50f;

    private void Start()
    {
        PositionObject();  
    }

    private void Update()
    {
        PositionObject();

        // если пользователь нажмет правую кнопку мыши, то мы будем удалять этот скрипт и тем самым мы и будем располагать
        // само по себе здание

        if (Input.GetMouseButtonDown(0))
            Destroy(gameObject.GetComponent<PlaceObjects>());

        // сделаем возможность вращения наших построек

        if(Input.GetKey(KeyCode.C))
            transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);


    }

    private void PositionObject()
    {
        // тут мы реализовываем передвижение самого домика,
        // в то место куда смотрит наша камера

        // преобразование координат 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        // тут мы выпускаем виртуальный луч(когда этот луч соприкоснется с нашей поверхностью мы сможем
        // отследить точку соприкосновения и эти мы понимаем куда смотрит наша мышка)

        if (Physics.Raycast(ray, out hit, 1000f, layer))

        // 1000f - это расстояние на
        // которое будет стрелять наш луч
        {  // меняем позицию объекта на котором находится этот скрипт

            transform.position = hit.point; // point это Vector3 - это то место
                                            // куда и попал наш луч
        }
    }
}
