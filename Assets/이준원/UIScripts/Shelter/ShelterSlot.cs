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
    //���� ��ũ��Ʈ�� �� ���� �ٲ� �ٸ� ��ũ��Ʈ�� �־ ���Ϳ��� Ŭ���ص� �κ��丮 ���� ������ �Ұ�
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
            Debug.Log(e + "�������");
        }
    
    }
    */
}
