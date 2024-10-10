using System.Collections; // �߰�: IEnumerator�� ����ϱ� ���� ���ӽ����̽�
using UnityEngine;

public class CoroutineManager : MonoBehaviour
{
    // �ڷ�ƾ�� �����ϴ� �޼���
    public void StartRotationCoroutine(GameObject obj, float seconds)
    {
        if (this.gameObject.activeInHierarchy)
        {
            StartCoroutine(RotateForSeconds(obj, seconds));
        }
        else
        {
            Debug.LogError("CoroutineManager�� ��Ȱ��ȭ�� �����Դϴ�.");
        }
    }

    // �ڷ�ƾ ���� �κ�
    private IEnumerator RotateForSeconds(GameObject obj, float seconds)
    {
        float elapsedTime = 0f;
        while (elapsedTime < seconds)
        {
            // �� �����Ӹ��� ��ü�� ȸ����Ŵ
            obj.transform.Rotate(new Vector3(0, 0, -500) * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // ���� �ð��� ���� �� ��ü ��Ȱ��ȭ
        obj.SetActive(false);
    }
}
