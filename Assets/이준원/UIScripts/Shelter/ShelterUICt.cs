using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelterUICt : MonoBehaviour
{
    public static ShelterUICt instance;
    public GameObject UIPanel;

    private void Awake()
    {
        instance = this;
    }
    public void UiOn()
    {
        UIPanel.SetActive(true);
    }

    public void UiOff()
    {
        UIPanel.SetActive(false);
    }
}
