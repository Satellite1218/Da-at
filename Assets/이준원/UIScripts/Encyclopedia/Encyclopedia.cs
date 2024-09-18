using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encyclopedia : MonoBehaviour
{
    #region Singleton
    public static Encyclopedia instance;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion

    public delegate void OnSlotCountChange(int val);
    public OnSlotCountChange onSlotCountChange;

    public delegate void OnChangeItem(); // 인벤에 아이템 추가시 슬롯에도 추가하는 코드로 추정
    public OnChangeItem onChangeItem; // 이거 활용해서 데이터 이동방식 변경해보자
    
    public List<Item> items = new List<Item>();
    private int slotCnt;

    public EncyclopediaUI EncUI;
    public int SlotCnt
    {
        get => slotCnt;
        set
        {
            slotCnt = value;
            onSlotCountChange.Invoke(slotCnt);
        }
    }


    private void Start()
    {
        slotCnt = ItemDatabase.instance.itemDB.Count;
        SetEnc();
    }

    public void SetEnc()
    {
        for (int i = 0; i < slotCnt; i++)
        {
            AddItem(ItemDatabase.instance.itemDB[i]);
        }
        SetImage();
    }
    
    public void DebugOpenAll()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].getChecked = true;
        }
    }
    public void SetImage()
    {
        for (int i = 0; i < slotCnt; i++)
        {
            if (!items[i].getChecked)
            {
                items[i].itemImage = null;
            }
            else
            {
                items[i].itemImage = ItemDatabase.instance.itemDB[i].itemImage;
            }
        }
    }

    
    public bool AddItem(Item _item)
    {
        if (items.Count < SlotCnt)
        {
            EncUI.AddSlot();
            items.Add(_item);
            if (onChangeItem != null)
            onChangeItem.Invoke();
            return true;
        } 
        return false;
    }

}
