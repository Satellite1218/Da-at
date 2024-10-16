using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickCheck : MonoBehaviour
{
    void Update()
    {
        // 마우스 왼쪽 버튼 클릭 감지
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("click");
            AudioManager.instance.PlaySfx(AudioManager.Sfx.genButton3);
        }
    }
}
