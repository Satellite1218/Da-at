using UnityEngine;
using UnityEngine.UI;

public class Uionoff : MonoBehaviour
{
    // Canvas 오브젝트를 할당할 변수
    public GameObject shopCanvas;

    // 상점 버튼을 할당할 변수
    public Button shopButton;

    // 닫기 버튼을 할당할 변수
    public Button closeButton;

    void Start()
    {
        // 시작할 때 Canvas를 비활성화 상태로 설정
        shopCanvas.SetActive(false);

        // 버튼 클릭 이벤트 등록
        shopButton.onClick.AddListener(OpenShop);
        closeButton.onClick.AddListener(CloseShop);
    }

    // 상점을 여는 함수
    void OpenShop()
    {
        shopCanvas.SetActive(true);
    }

    // 상점을 닫는 함수
    void CloseShop()
    {
        shopCanvas.SetActive(false);
    }

    void OnApplicationQuit()
    {
        // 실행 중지 시 Canvas를 비활성화
        shopCanvas.SetActive(false);
    }
}
