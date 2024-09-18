using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI instance;

    public SlotPlus SP;

    Inventory inven;

    public Slot[] slots;
    public Transform slotHorder;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        inven = Inventory.instance;
        slots = slotHorder.GetComponentsInChildren<Slot>();
        inven.onSlotCountChange += SlotChange;
        inven.onChangeItem += RedrawSlotUI;
        for (int i = 0; i < slots.Length; i++)
            slots[i].slotNum = i;
    }

    private void SlotChange(int val) // ¿ø·¡´Â private¿´´Ù.
    {
        for (int i = 0; i <slots.Length; i++)
        {
            slots[i].slotNum = i;
            if (i < inven.SlotCnt)
            {
                slots[i].GetComponent<Button>().interactable = true;
            }
            else
            {
                slots[i].GetComponent<Button>().interactable = false;
            }
        }
    }

    public void AddSlot()
    {
        inven.SlotCnt += 8;
        for (int i = 0; i < 8; i++)
            SP.PlusSlot();
        slots = slotHorder.GetComponentsInChildren<Slot>();
        SlotChange(1);
    }

    public void AddItemTest(int e)
    {
        if (e == 0)
            if (Inventory.instance.AddItem(ItemDatabase.instance.itemDB[0]))
                Debug.Log("´ãºñÆ÷È¹");
        if (e == 1)
            if (Inventory.instance.AddItem(ItemDatabase.instance.itemDB[1]))
                Debug.Log("°ñµç¹ÚÁãÆ÷È¹");
        if (e == 2)
            if (Inventory.instance.AddItem(ItemDatabase.instance.itemDB[2]))
                Debug.Log("º£¾îÆ÷È¹");
        if (e == 3)
            if (Inventory.instance.AddItem(ItemDatabase.instance.itemDB[3]))
                Debug.Log("º£¾îÆ÷È¹");
        if (e == 4)
            if (Inventory.instance.AddItem(ItemDatabase.instance.itemDB[4]))
                Debug.Log("º£¾îÆ÷È¹");
    }

    void RedrawSlotUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveSlot();
        }
        for (int i = 0; i < inven.items.Count; i++) 
        {
            slots[i].item = inven.items[i];
            slots[i].UpdateSlotUI();
        }
    }

}
