using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ShelterSlot : MonoBehaviour//, IPointerClickHandler
{
    public int slotNum;
    public Item item;
    public Image itemIcon;

    public void UpdateSlotUI()
    {
        itemIcon.sprite = item.itemImage;
        itemIcon.gameObject.SetActive(true);
    }

    public void RemoveSlot()
    {
        item = null;
        itemIcon.gameObject.SetActive(false);
    }

    /*
    //하위 스크립트들 다 내용 바꾼 다른 스크립트로 넣어서 쉘터에서 클릭해도 인벤토리 영향 없도록 할것
    public void OnPointerClick(PointerEventData eventData)
    {
        try
        {
            bool isSold = item.Sell();
            if (isSold)
                Inventory.instance.RemoveItem(slotNum);
        }
        catch (Exception e)
        {
            Debug.Log(e + "비어있음");
        }
    
    }
    */
}
