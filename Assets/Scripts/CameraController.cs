using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // скорость вращения камеры      // ускорение   // скорость приближения и отдаления камеры
    public float rotateSpeed = 10.0f, speed = 10.0f, zoomSpeed = 10.0f;
    

    private float _mult = 1f; // скорость по умолчанию

    // метод для вращения камеры
    private void Update()
    {

        // создаем переменную для того чтобы если пользователь не будет нажимать на какие-нибудь клавиши
        // связанные с поворотом камеры, то камера будет стоять на месте
        float rotate = 0f;

        // Передвижение камеры вверх, вниз, влево, вправо
        float hor = Input.GetAxis("Horizontal"); // данное встроенное значение позволяет отслеживать такие клавиши как 
                                                 // стрелочки вправо и влево и клавиши A D
        float ver = Input.GetAxis("Vertical"); // Vertical также позволяет отслеживать стрелочки вверх и вниз и клавиши W S 

        // GetKey используем для постоянного нажатия  на данную клавишу
        if (Input.GetKey(KeyCode.Q)) // поворот камеры влево
            rotate = -1f;
        else if (Input.GetKey(KeyCode.E)) // поворот камеры вправо
            rotate = 1f;

        // проверяем, если пользователь зажал левый шифт то она будет ускоряться, иначе будет двигаться с обычной скоростью
        _mult = Input.GetKey(KeyCode.LeftShift) ? 2f : 1f;

        // up - повзволяет менять координату y, затем умножаем на скорость вращения камеры
        // Time.deltaTime - позволяет добавить плавности к какому-либо действию
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime * rotate * _mult, Space.World);
        // для вращения камеры относительно глобальныйх координат мы указываем параметр Space.World
        transform.Translate(new Vector3(hor, 0, ver) * Time.deltaTime * _mult * speed, Space.Self);
        //метод Translate позволяет передвигать объект
        // Self мы указали
        // для локальных координат
        // для того чтобы камера
        // двигалась в ту сторону
        // куда мы смотрели

        // тут мы делаем приближение и отдаление камеры c помощью скролла мыши
        transform.position += transform.up * zoomSpeed * Time.deltaTime * Input.GetAxis("Mouse ScrollWheel");
        // up это значение по Y-ку 

        //ограничение скролла камеры
        transform.position = new Vector3(
            transform.position.x,
            Mathf.Clamp(transform.position.y, -20f, 30f),
            transform.position.z);
    }
}
