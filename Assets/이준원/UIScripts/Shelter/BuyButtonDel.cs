using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyButtonDel : MonoBehaviour
{
    GameObject BuySlotButton;

    private void Start()
    {
        BuySlotButton = GameObject.Find("BuySlotButton");
    }
    public void BuySlotButtonDel()
    {
        Debug.Log("asf");
        BuySlotButton.SetActive(false);
    }

}
