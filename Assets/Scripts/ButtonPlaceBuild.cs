using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPlaceBuild : MonoBehaviour
{
    public GameObject building;

    // создание публичных методов позволяет вызываться при нажатии на кнопку, а вызвать при нажатии на кнопку можно только
    // публичные методы
    public void PlaceBuild()
    {  // метод "Instantiate" - позволяет создать некий префаб(игровой объект)
        Instantiate(building, Vector3.zero, Quaternion.identity); // "Quaternion" - позволяет создать начальное
                                                                  // вращение, но у нас никакого начального
                                                                  // вращения не будет, т.к у нас стоит ".identity"
    }
}
