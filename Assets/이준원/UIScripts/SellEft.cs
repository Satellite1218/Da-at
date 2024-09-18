using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ItemEft/sold")]
public class SellEft : ItemEffect
{
    AnimalShelter An;
    public override bool ExecuteRole(int _price)
    {
        //요기요기서 리아이템
        Debug.Log(_price + "골드를 획득했습니다.");
        return true;
    }
}
