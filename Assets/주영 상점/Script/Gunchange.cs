using UnityEngine;

public class Gunchage : MonoBehaviour
{
    public GameObject externalObject; // UI 외부의 오브젝트
    public Sprite[] sprites; // 교체할 스프라이트 배열
    private SpriteRenderer externalSpriteRenderer; // 외부 객체의 스프라이트 렌더러
    private int currentIndex = 0; // 현재 스프라이트 인덱스

    private string currentIndexKey;

    void Start()
    {
        // 외부 객체의 SpriteRenderer를 가져오기
        if (externalObject != null)
        {
            externalSpriteRenderer = externalObject.GetComponent<SpriteRenderer>();
        }

        // 객체마다 고유한 키 생성 (예: 객체 이름 사용)
        currentIndexKey = $"{externalObject.name}_CurrentImageIndex";

        // 저장된 인덱스를 불러와서 설정
        currentIndex = PlayerPrefs.GetInt(currentIndexKey, 0);
        UpdateSprite();
    }

    // 특정 인덱스로 스프라이트 변경 (UI에서 호출)
    public void ChangeSpriteToIndex(int index)
    {
        if (index >= 0 && index < sprites.Length)
        {
            currentIndex = index;
            UpdateSprite();

            // 현재 인덱스를 저장
            PlayerPrefs.SetInt(currentIndexKey, currentIndex);
            PlayerPrefs.Save();
        }
    }

    private void UpdateSprite()
    {
        // 외부 객체의 스프라이트 업데이트
        if (externalSpriteRenderer != null && currentIndex >= 0 && currentIndex < sprites.Length)
        {
            externalSpriteRenderer.sprite = sprites[currentIndex];
        }
    }

    public void ResetSprite()
    {
        // 스프라이트 인덱스 초기화
        currentIndex = 0;
        UpdateSprite();

        // 초기 인덱스를 저장
        PlayerPrefs.SetInt(currentIndexKey, currentIndex);
        PlayerPrefs.Save();
    }
}
