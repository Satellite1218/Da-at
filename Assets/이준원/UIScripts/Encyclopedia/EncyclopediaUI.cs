using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EncyclopediaUI : MonoBehaviour
{
    #region Singleton
    public static EncyclopediaUI instance;
    private void Awake()
    {
        DontDestroyOnLoad(transform.root.gameObject);

        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion
    public SlotPlus SP;

    Encyclopedia Enc;
    public GameObject descUI;

    public EncSlot[] slots;
    public Transform slotHorder;
    private void Start()
    {
        Enc = Encyclopedia.instance;
        // 고려할것 -> descUI = FindObjectOfType
        slots = slotHorder.GetComponentsInChildren<EncSlot>();
        Enc.onSlotCountChange += SlotChange;
        Enc.onChangeItem += RedrawSlotUI;
        for (int i = 0; i < slots.Length; i++)
            slots[i].slotNum = i;
    }


    public void Description(int descNum) //-------------------------------
    {
        Debug.Log(descNum);
        descUI.SetActive(true);
        DescSet.instance.DescSett(descNum);
        descUI.SetActive(true);
    }



    private void SlotChange(int val) // 원래는 private였다.
    {
        for (int i = 0; i <slots.Length; i++)
        {
            slots[i].slotNum = i;
            if (i < Enc.SlotCnt)
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
        SP.PlusSlot();
        slots = slotHorder.GetComponentsInChildren<EncSlot>();
        SlotChange(1);
    }

    void RedrawSlotUI() //오류 발생 시 퍼블릭 제거
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveSlot();
        }
        for (int i = 0; i < Enc.items.Count; i++) 
        {
            slots[i].item = Enc.items[i];
            slots[i].UpdateSlotUI();
        }
    }

}
