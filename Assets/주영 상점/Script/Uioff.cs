using UnityEngine;
using UnityEngine.UI;

public class ShopUIController : MonoBehaviour
{
    // Canvas 오브젝트를 할당할 변수
    public GameObject shopCanvas;

    // Close 버튼 오브젝트를 할당할 변수
    public Button closeButton;

    void Start()
    {
        // 버튼에 클릭 이벤트 리스너를 추가
        closeButton.onClick.AddListener(CloseShopUI);
    }

    // Close 버튼을 클릭할 때 호출되는 함수
    void CloseShopUI()
    {
        shopCanvas.SetActive(false);
    }
}
