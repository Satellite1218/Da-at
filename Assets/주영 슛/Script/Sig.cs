using UnityEngine;
using System.Collections;

public class Sig : MonoBehaviour
{
    public bool reloading = false;
    public GameObject targetObject; // 보이게 할 객체 참조

    public void Reloadingcome(bool value)
    {
        reloading = value;
        if (reloading)
        {
            if (targetObject != null)
            {
                // 객체를 활성화
                targetObject.SetActive(true);
                // 회전 코루틴 시작
                StartCoroutine(RotateForSeconds(targetObject, 4f));
            }
        }
        Debug.Log($"Reloadingcome called, reloading set to {value}");
    }

    private IEnumerator RotateForSeconds(GameObject obj, float seconds)
    {
        float elapsedTime = 0f;
        while (elapsedTime < seconds)
        {
            // 매 프레임마다 객체를 회전
            obj.transform.Rotate(new Vector3(0, 0, -500) * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // 3초가 지난 후에 객체를 비활성화
        obj.SetActive(false);
    }
}
