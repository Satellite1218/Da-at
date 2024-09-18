using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class EncSlot : MonoBehaviour, IPointerClickHandler
{
    public int slotNum;
    public Item item;
    public Image itemIcon;


    private void Start()
    {
        UpdateSlotUI();
    }
    private void Update() //방식 변경 필요
    {
        if (item.getChecked)
        {
            UpdateSlotUI();
        }
    }
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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Encyclopedia.instance.items[slotNum].getChecked)
        {
            EncyclopediaUI.instance.Description(slotNum);
        }
    }
}
