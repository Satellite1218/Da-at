using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalShelterUI : MonoBehaviour
{

    //public static AnimalShelterUI instance; <- 유지 필요하면 이걸로 넣기

    AnimalShelter AnSh;
    public BuySlot BS;
    public BuyButtonDel BBD;

    public ShelterSlot[] slots;
    public Transform slotHorder;

    private void Start()
    {
        AnSh = AnimalShelter.instance;
        slots = slotHorder.GetComponentsInChildren<ShelterSlot>();
        AnSh.onSlotCountChange += SlotChange;
        AnSh.onChangeItem += RedrawSlotUI;
    }
    private void SlotChange(int val)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotNum = i;
            if (i < AnSh.SlotCnt)
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
        if (!AnSh.AddSlotCnt())
        {
            Debug.Log("칸다참");
            return;
        }
        BS.PlusSlotShelter();
        slots = slotHorder.GetComponentsInChildren<ShelterSlot>();
        if (AnSh.SlotCnt == 8)
        {
            Debug.Log("ddd");
            BBD.BuySlotButtonDel();
        }
    }


    void RedrawSlotUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveSlot();
        }
        for (int i = 0; i < AnSh.items.Count; i++)
        {
            slots[i].item = AnSh.items[i];
            slots[i].UpdateSlotUI();
        }
    }

}
