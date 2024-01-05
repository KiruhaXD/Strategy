using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPlaceBuild : MonoBehaviour
{
    public GameObject building;

    // �������� ��������� ������� ��������� ���������� ��� ������� �� ������, � ������� ��� ������� �� ������ ����� ������
    // ��������� ������
    public void PlaceBuild()
    {  // ����� "Instantiate" - ��������� ������� ����� ������(������� ������)
        Instantiate(building, Vector3.zero, Quaternion.identity); // "Quaternion" - ��������� ������� ���������
                                                                  // ��������, �� � ��� �������� ����������
                                                                  // �������� �� �����, �.� � ��� ����� ".identity"
    }
}
