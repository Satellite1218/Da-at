using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public Item bebsae, dambe;
    public GameObject prefab; // 소환할 프리팹

    public void Beb()
    {
        // 인벤토리에 아이템 추가
        Inventory.instance.AddItem(bebsae);
    }
    public void Dam()
    {
        // 인벤토리에 아이템 추가
        Inventory.instance.AddItem(dambe);
    }

    void Start()
    {
        // 각각의 Dambe 객체가 자신의 아이템을 설정하도록 변경
        bebsae = ItemDatabase.instance.itemDB[1];
        dambe = ItemDatabase.instance.itemDB[3];
    }
}
