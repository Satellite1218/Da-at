using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public Item bebsae, dambe;
    public GameObject prefab; // ��ȯ�� ������

    public void Beb()
    {
        // �κ��丮�� ������ �߰�
        Inventory.instance.AddItem(bebsae);

        // �ı��ϱ� ���� �θ� ���� ����
        transform.SetParent(null);
    }
    public void Dam()
    {
        // �κ��丮�� ������ �߰�
        Inventory.instance.AddItem(dambe);

        // �ı��ϱ� ���� �θ� ���� ����
        transform.SetParent(null);
    }

    void Start()
    {
        // ������ Dambe ��ü�� �ڽ��� �������� �����ϵ��� ����
        bebsae = ItemDatabase.instance.itemDB[1];
        dambe = ItemDatabase.instance.itemDB[3];
    }
}
