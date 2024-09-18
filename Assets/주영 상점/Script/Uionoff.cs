using UnityEngine;

public class Uionoff : MonoBehaviour
{
    // Canvas 오브젝트를 할당할 변수
    public GameObject shopCanvas;

    // Shop 스프라이트 오브젝트를 할당할 변수
    public GameObject shopSprite;

    void Start()
    {
        // 시작할 때 Canvas를 비활성화 상태로 설정
        shopCanvas.SetActive(false);
    }

    void Update()
    {
        // 마우스 클릭을 감지
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 rayPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(rayPos), Vector2.zero);

            if (hit && hit.transform.gameObject == shopSprite)
            {
                // Shop 스프라이트를 클릭했을 때 Canvas를 활성화
                shopCanvas.SetActive(true);
            }
        }
    }

    void OnApplicationQuit()
    {
        // 실행 중지 시 Canvas를 비활성화
        shopCanvas.SetActive(false);
    }
}
