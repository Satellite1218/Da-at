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

    public delegate void OnChangeItem(); // �κ��� ������ �߰��� ���Կ��� �߰��ϴ� �ڵ�� ����
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

    //public Text totalPriceText; // ��ü ������ ǥ���� UI �ؽ�Ʈ
    public Image itemIcon; // UI�� �̹����� ǥ���� ������Ʈ
    public Text itemNameText; // UI�� ������ �̸��� ǥ���� ������Ʈ
    public Text itemPriceText; // UI�� ������ ������ ǥ���� ������Ʈ
    public Text itemDescriptText; //UI�� ������ ������ ǥ���� ������Ʈ

    public Button totalPriceButton; // �� ���� ����� ���� ��ư

    private void Start()
    {
        slotCnt = 32;
        // ��ư Ŭ�� �� UpdateTotalPrice �޼��带 ȣ���ϵ��� ������ �߰�
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

    // �κ��丮 �� ��ü �������� ���� �հ踦 ����ϰ� ������Ʈ�ϴ� �޼���
    private void UpdateTotalPrice()
    {
        int totalPrice = 0; // ��ü ���� �ʱ�ȭ
        foreach (var item in items)
        {
            totalPrice += item.price; // �� �������� ������ ����
        }

        // UI�� �� ���� ǥ��
        //totalPriceText.text = "�� ����: " + totalPrice.ToString();
        Debug.Log("TP" + totalPrice);
    }

    // ���� �ֱٿ� �߰��� �������� ������ ������Ʈ�ϴ� �޼���
    private void UpdateItem()
    {
        if (items.Count == 0) return; // �κ��丮�� ����ִ� ��� ó��

        // ���� �ֱٿ� �߰��� �������� �̹����� �����ͼ� UI�� ǥ��
        itemIcon.sprite = items[items.Count - 1].itemImage;
        itemIcon.preserveAspect = true; // �̹��� ������ �����ϰ� ����
        itemIcon.gameObject.SetActive(true); // ������ Ȱ��ȭ

        // ���� �ֱٿ� �߰��� �������� �̸��� �����ͼ� UI�� ǥ��
        itemNameText.text = items[items.Count - 1].itemName;
        itemNameText.gameObject.SetActive(true); // �̸� �ؽ�Ʈ Ȱ��ȭ

        // ���� �ֱٿ� �߰��� �������� ������ �����ͼ� UI�� ǥ��
        itemPriceText.text = items[items.Count - 1].price.ToString() + "��";
        itemPriceText.gameObject.SetActive(true); // ���� �ؽ�Ʈ Ȱ��ȭ

        // ���� �ֱٿ� �߰��� �������� ������ �����ͼ� UI�� ǥ��
        itemDescriptText.text = items[items.Count - 1].itemText;
        itemDescriptText.gameObject.SetActive(true); // ���� �ؽ�Ʈ Ȱ��ȭ
    }
}
