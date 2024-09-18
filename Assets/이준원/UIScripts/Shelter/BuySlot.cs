using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class BuySlot : MonoBehaviour, IPointerClickHandler
{

    [SerializeField] GameObject itemSlotPrefab;
    [SerializeField] Transform itemsParent;

    public AnimalShelterUI ASUI;

    public int money = 400;
    public int price = 300;
    public void OnPointerClick(PointerEventData eventData) 
    {

        if (money >= price)
        {
            Debug.Log("µ·ÃæºÐ");
            ASUI.AddSlot();

        }
        else if (money < price)
        {
            Debug.Log("µ·ºÎÁ·");
        }
    }

    public void PlusSlotShelter() 
    {

        GameObject itemSlotGameObj = Instantiate(itemSlotPrefab);
        itemSlotGameObj.transform.SetParent(itemsParent, worldPositionStays: false);
    }
}
