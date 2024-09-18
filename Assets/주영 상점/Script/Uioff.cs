using UnityEngine;
using UnityEngine.UI;

public class ShopUIController : MonoBehaviour
{
    // Canvas ������Ʈ�� �Ҵ��� ����
    public GameObject shopCanvas;

    // Close ��ư ������Ʈ�� �Ҵ��� ����
    public Button closeButton;

    void Start()
    {
        // ��ư�� Ŭ�� �̺�Ʈ �����ʸ� �߰�
        closeButton.onClick.AddListener(CloseShopUI);
    }

    // Close ��ư�� Ŭ���� �� ȣ��Ǵ� �Լ�
    void CloseShopUI()
    {
        shopCanvas.SetActive(false);
    }
}
