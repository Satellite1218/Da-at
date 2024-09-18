using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DescSet : MonoBehaviour
{
    public static DescSet instance;

    public Image descImage;
    public TMP_Text descName;
    public TMP_Text descText;

    private int itemNum = 0;
    private bool plagTypeText = true; //true이 인게임, false이 현실

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //private Sprite newSprite;
    public void DescSett(int _descNum)
    {
        itemNum = _descNum;
        //newSprite = ItemDatabase.instance.itemDB[_descNum].itemImage;

        descImage.sprite = ItemDatabase.instance.itemDB[_descNum].itemImage;
        descName.text = ItemDatabase.instance.itemDB[_descNum].itemName;
        descText.text = ItemDatabase.instance.itemDB[_descNum].itemText;
        plagTypeText = true;

        //Image.sprite = newSprite;
    }

    public void DescTextSett()
    {
        if (plagTypeText)
        {
            descText.text = ItemDatabase.instance.itemDB[itemNum].itemRealText;
            plagTypeText = false;
        }
        else if (!plagTypeText)
        {
            descText.text = ItemDatabase.instance.itemDB[itemNum].itemText;
            plagTypeText = true;
        }

    }
}
