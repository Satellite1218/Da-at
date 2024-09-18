using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelterIns : MonoBehaviour
{
    #region Singleton
    public static ShelterIns instance;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        //InvenUi = GameObject.Find("InventoryCt");

        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion
}
