using UnityEngine;
using UnityEngine.UI;

public class Uionoff : MonoBehaviour
{
    // Canvas ������Ʈ�� �Ҵ��� ����
    public GameObject shopCanvas;

    // ���� ��ư�� �Ҵ��� ����
    public Button shopButton;

    // �ݱ� ��ư�� �Ҵ��� ����
    public Button closeButton;

    void Start()
    {
        // ������ �� Canvas�� ��Ȱ��ȭ ���·� ����
        shopCanvas.SetActive(false);

        // ��ư Ŭ�� �̺�Ʈ ���
        shopButton.onClick.AddListener(OpenShop);
        closeButton.onClick.AddListener(CloseShop);
    }

    // ������ ���� �Լ�
    void OpenShop()
    {
        shopCanvas.SetActive(true);
    }

    // ������ �ݴ� �Լ�
    void CloseShop()
    {
        shopCanvas.SetActive(false);
    }

    void OnApplicationQuit()
    {
        // ���� ���� �� Canvas�� ��Ȱ��ȭ
        shopCanvas.SetActive(false);
    }
}
