using UnityEngine;

public class Gunchage : MonoBehaviour
{
    public GameObject externalObject; // UI �ܺ��� ������Ʈ
    public Sprite[] sprites; // ��ü�� ��������Ʈ �迭
    private SpriteRenderer externalSpriteRenderer; // �ܺ� ��ü�� ��������Ʈ ������
    private int currentIndex = 0; // ���� ��������Ʈ �ε���

    private string currentIndexKey;

    void Start()
    {
        // �ܺ� ��ü�� SpriteRenderer�� ��������
        if (externalObject != null)
        {
            externalSpriteRenderer = externalObject.GetComponent<SpriteRenderer>();
        }

        // ��ü���� ������ Ű ���� (��: ��ü �̸� ���)
        currentIndexKey = $"{externalObject.name}_CurrentImageIndex";

        // ����� �ε����� �ҷ��ͼ� ����
        currentIndex = PlayerPrefs.GetInt(currentIndexKey, 0);
        UpdateSprite();
    }

    // Ư�� �ε����� ��������Ʈ ���� (UI���� ȣ��)
    public void ChangeSpriteToIndex(int index)
    {
        if (index >= 0 && index < sprites.Length)
        {
            currentIndex = index;
            UpdateSprite();

            // ���� �ε����� ����
            PlayerPrefs.SetInt(currentIndexKey, currentIndex);
            PlayerPrefs.Save();
        }
    }

    private void UpdateSprite()
    {
        // �ܺ� ��ü�� ��������Ʈ ������Ʈ
        if (externalSpriteRenderer != null && currentIndex >= 0 && currentIndex < sprites.Length)
        {
            externalSpriteRenderer.sprite = sprites[currentIndex];
        }
    }

    public void ResetSprite()
    {
        // ��������Ʈ �ε��� �ʱ�ȭ
        currentIndex = 0;
        UpdateSprite();

        // �ʱ� �ε����� ����
        PlayerPrefs.SetInt(currentIndexKey, currentIndex);
        PlayerPrefs.Save();
    }
}
