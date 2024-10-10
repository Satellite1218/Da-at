using System.Collections; // 추가: IEnumerator를 사용하기 위한 네임스페이스
using UnityEngine;

public class CoroutineManager : MonoBehaviour
{
    // 코루틴을 시작하는 메서드
    public void StartRotationCoroutine(GameObject obj, float seconds)
    {
        if (this.gameObject.activeInHierarchy)
        {
            StartCoroutine(RotateForSeconds(obj, seconds));
        }
        else
        {
            Debug.LogError("CoroutineManager가 비활성화된 상태입니다.");
        }
    }

    // 코루틴 실행 부분
    private IEnumerator RotateForSeconds(GameObject obj, float seconds)
    {
        float elapsedTime = 0f;
        while (elapsedTime < seconds)
        {
            // 매 프레임마다 객체를 회전시킴
            obj.transform.Rotate(new Vector3(0, 0, -500) * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // 일정 시간이 지난 후 객체 비활성화
        obj.SetActive(false);
    }
}
