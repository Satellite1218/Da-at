using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType // ���̵�����
{
    Mammalia, // ������
    Amphibia, // �缭��
    Birds // ����
}
[System.Serializable]
public class Item
{
    public int itemId; // ���̵�
    public int itemLv; // ���� ����
    public ItemType itemType; // ���������߰� -> �ؽ�Ʈ������Ʈ ������
    public string itemName; // �����̸�
    public Sprite itemImage; // �����̹���

    public int price;

    public string spownArea; //���� ����
    public int spownRange; // ���� �Ÿ�

    public string itemText; //�ΰ��� ����
    public string itemRealText; // ���� ����

    public bool getChecked = false; // ���� ȹ�� �÷���
    public List<ItemEffect> efts; //�����ۺ� ���ȿ���� ����� ����


    public bool Sell()
    {
        bool isSold = false;
        foreach (ItemEffect eft in efts)
        {
            isSold = eft.ExecuteRole(price);
        }

        return isSold;
    }

    public bool Storage()
    {
        bool isMoved = false;
        isMoved = true;

        return isMoved;
    }
}
