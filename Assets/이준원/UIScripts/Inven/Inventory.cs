using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;
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
    public OnChangeItem onChangeItem;

    public List<Item> items = new List<Item>();
    private int slotCnt;

    public InventoryUI InvenUi;
    public int SlotCnt
    {
        get => slotCnt;
        set
        {
            slotCnt = value;
            onSlotCountChange.Invoke(slotCnt);
        }
    }

    //public Text totalPriceText; // 전체 가격을 표시할 UI 텍스트
    public Image itemIcon; // UI에 이미지를 표시할 컴포넌트
    public Text itemNameText; // UI에 아이템 이름을 표시할 컴포넌트
    public Text itemPriceText; // UI에 아이템 가격을 표시할 컴포넌트
    public Text itemDescriptText; //UI에 아이템 설명을 표시할 컴포넌트

    public Button totalPriceButton; // 총 가격 계산을 위한 버튼

    private void Start()
    {
        slotCnt = 32;
        // 버튼 클릭 시 UpdateTotalPrice 메서드를 호출하도록 리스너 추가
        totalPriceButton.onClick.AddListener(UpdateTotalPrice);
    }

    public bool AddItem(Item _item)
    {
        int num = _item.itemId;
        Encyclopedia.instance.items[num].getChecked = true;
        Encyclopedia.instance.SetImage();

        if (items.Count >= SlotCnt)
        {
            InvenUi.AddSlot();
        }
        if (items.Count < SlotCnt)
        {
            items.Add(_item);
            if (onChangeItem != null)
                onChangeItem.Invoke();
            UpdateItem();
            return true;
        }
        return false;
    }
    public void RemoveItem(int _index)
    {
        AnimalShelter.instance.AddItem(items[_index]);
        items.RemoveAt(_index);
        onChangeItem.Invoke();
    }

    public void RealRemoveItem(int _index)
    {
        items.RemoveAt(_index);
        onChangeItem.Invoke();
        UpdateItem();
    }

    //------------------------------

    // 인벤토리 내 전체 아이템의 가격 합계를 계산하고 업데이트하는 메서드
    private void UpdateTotalPrice()
    {
        int totalPrice = 0; // 전체 가격 초기화
        foreach (var item in items)
        {
            totalPrice += item.price; // 각 아이템의 가격을 더함
        }

        // UI에 총 가격 표시
        //totalPriceText.text = "총 가격: " + totalPrice.ToString();
        Debug.Log("TP" + totalPrice);
    }

    // 가장 최근에 추가된 아이템의 정보를 업데이트하는 메서드
    private void UpdateItem()
    {
        if (items.Count == 0) return; // 인벤토리가 비어있는 경우 처리

        // 가장 최근에 추가된 아이템의 이미지를 가져와서 UI에 표시
        itemIcon.sprite = items[items.Count - 1].itemImage;
        itemIcon.preserveAspect = true; // 이미지 비율을 유지하게 설정
        itemIcon.gameObject.SetActive(true); // 아이콘 활성화

        // 가장 최근에 추가된 아이템의 이름을 가져와서 UI에 표시
        itemNameText.text = items[items.Count - 1].itemName;
        itemNameText.gameObject.SetActive(true); // 이름 텍스트 활성화

        // 가장 최근에 추가된 아이템의 가격을 가져와서 UI에 표시
        itemPriceText.text = items[items.Count - 1].price.ToString() + "원";
        itemPriceText.gameObject.SetActive(true); // 가격 텍스트 활성화

        // 가장 최근에 추가된 아이템의 설명을 가져와서 UI에 표시
        itemDescriptText.text = items[items.Count - 1].itemText;
        itemDescriptText.gameObject.SetActive(true); // 설명 텍스트 활성화
    }
}
