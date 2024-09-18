using UnityEngine;
using System.Collections;

public class Sig : MonoBehaviour
{
    public bool reloading = false;
    public GameObject targetObject; // ���̰� �� ��ü ����

    public void Reloadingcome(bool value)
    {
        reloading = value;
        if (reloading)
        {
            if (targetObject != null)
            {
                // ��ü�� Ȱ��ȭ
                targetObject.SetActive(true);
                // ȸ�� �ڷ�ƾ ����
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
            // �� �����Ӹ��� ��ü�� ȸ��
            obj.transform.Rotate(new Vector3(0, 0, -500) * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // 3�ʰ� ���� �Ŀ� ��ü�� ��Ȱ��ȭ
        obj.SetActive(false);
    }
}
