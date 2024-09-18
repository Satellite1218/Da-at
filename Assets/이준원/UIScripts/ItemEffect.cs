using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemEffect : ScriptableObject
{
    public abstract bool ExecuteRole(int _price); // 판매, 이송 결과 구분용으로 쓸지 그냥 판매용으로 쓸지 고민중 (price)
}
