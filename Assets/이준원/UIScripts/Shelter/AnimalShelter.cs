using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalShelter : MonoBehaviour
{
    #region Singleton
    public static AnimalShelter instance;
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

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;

    public List<Item> items = new List<Item>();
    private int slotCnt;
    private int maxSlotCnt = 8;

    public AnimalShelterUI AnShUi;
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
        slotCnt = 3;
    }

    public bool AddSlotCnt()
    {
        if (SlotCnt < maxSlotCnt)
        {
            slotCnt++;
            return true;
        }
        else if (SlotCnt >= maxSlotCnt) // slotcnt가 maxslotcnt보다 크다면 같도록 만드는거 추가하기
        {
            return false;
        }
        return false;
    }

    public bool AddItem(Item _item)
    {
        if (items.Count < SlotCnt)
        {
            items.Add(_item);
            if (onChangeItem != null)
                onChangeItem.Invoke();
            return true;
        }
        return false;
    }

    public void RemoveItem(int _index)
    {
        items.RemoveAt(_index);
        onChangeItem.Invoke();

    }

}
