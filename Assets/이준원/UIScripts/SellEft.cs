using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ItemEft/sold")]
public class SellEft : ItemEffect
{
    AnimalShelter An;
    public override bool ExecuteRole(int _price)
    {
        //����⼭ ��������
        Debug.Log(_price + "��带 ȹ���߽��ϴ�.");
        return true;
    }
}
