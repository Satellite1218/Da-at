using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickCheck : MonoBehaviour
{
    void Update()
    {
        // ���콺 ���� ��ư Ŭ�� ����
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("click");
            AudioManager.instance.PlaySfx(AudioManager.Sfx.genButton3);
        }
    }
}
