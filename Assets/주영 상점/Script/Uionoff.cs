using UnityEngine;

public class Uionoff : MonoBehaviour
{
    // Canvas ������Ʈ�� �Ҵ��� ����
    public GameObject shopCanvas;

    // Shop ��������Ʈ ������Ʈ�� �Ҵ��� ����
    public GameObject shopSprite;

    void Start()
    {
        // ������ �� Canvas�� ��Ȱ��ȭ ���·� ����
        shopCanvas.SetActive(false);
    }

    void Update()
    {
        // ���콺 Ŭ���� ����
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 rayPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(rayPos), Vector2.zero);

            if (hit && hit.transform.gameObject == shopSprite)
            {
                // Shop ��������Ʈ�� Ŭ������ �� Canvas�� Ȱ��ȭ
                shopCanvas.SetActive(true);
            }
        }
    }

    void OnApplicationQuit()
    {
        // ���� ���� �� Canvas�� ��Ȱ��ȭ
        shopCanvas.SetActive(false);
    }
}
