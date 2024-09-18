using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EncLoad : MonoBehaviour, IPointerClickHandler
{
    public GameObject UIPanel;
    bool activeUI = false;


    private void Start()
    {
        UIPanel.SetActive(activeUI);   
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UiOnOff();
    }

    public void UiOnOff()
    {
        activeUI = !activeUI;
        UIPanel.SetActive(activeUI);
    }

}
