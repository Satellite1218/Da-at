using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public GameObject prefab; // ��ȯ�� ������

    public void Ani(int db)
    {
        // �κ��丮�� ������ �߰�
        Inventory.instance.AddItem(ItemDatabase.instance.itemDB[db]);

        Scription.instance.Turnon(); // ���� ��� �ٿ��
        // �ı��ϱ� ���� �θ� ���� ����
        transform.SetParent(null);
    }
}
