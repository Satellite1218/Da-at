using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType // 더미데이터
{
    Mammalia, // 포유류
    Amphibia, // 양서류
    Birds // 조류
}
[System.Serializable]
public class Item
{
    public int itemId; // 이이디
    public int itemLv; // 동물 레벨
    public ItemType itemType; // 도감설명추가 -> 텍스트오브젝트 생각중
    public string itemName; // 동물이름
    public Sprite itemImage; // 동물이미지

    public int price;

    public string spownArea; //스폰 지역
    public int spownRange; // 스폰 거리

    public string itemText; //인게임 설명
    public string itemRealText; // 현실 설명

    public bool getChecked = false; // 도감 획득 플래그
    public List<ItemEffect> efts; //아이템별 사용효과로 현재는 더미


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
