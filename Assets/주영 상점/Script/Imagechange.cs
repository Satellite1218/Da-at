using UnityEngine;
using UnityEngine.UI;

public class ImageChange : MonoBehaviour
{
    public Image targetImage; // UI에서 변경되는 이미지
    public Sprite[] evolutionSprites; // 업그레이드 스프라이트 배열
    private int currentIndex = 0;
    public int[] evolutionCosts; // 업그레이드 비용
    public Swordbuy swordbuy; // 돈 처리용 객체
    public Text costText; // 비용을 표시하는 텍스트

    // 외부 객체와 연동
    public Gunchage externalImageChanger; // 외부 객체의 스프라이트 변경을 처리하는 스크립트

    private string currentIndexKey;

    void Start()
    {
        // 객체마다 고유한 키 생성 (예: 객체 이름 사용)
        currentIndexKey = $"{gameObject.name}_CurrentImageIndex";

        // 저장된 인덱스를 불러와서 설정
        currentIndex = PlayerPrefs.GetInt(currentIndexKey, 0);
        UpdateImage();
        UpdateCostText();
    }

    public void OnPurchaseButtonClicked()
    {
        if (evolutionSprites.Length == 0 || evolutionCosts.Length == 0)
            return;

        if (currentIndex >= evolutionSprites.Length - 1)
            return;

        int cost = evolutionCosts[currentIndex];

        // 돈이 충분하면 구매하고 이미지 업데이트
        if (swordbuy.SpendMoney(cost))
        {
            currentIndex = Mathf.Min(currentIndex + 1, evolutionSprites.Length - 1);
            UpdateImage();

            // 객체별로 고유한 키를 사용하여 현재 인덱스를 저장
            PlayerPrefs.SetInt(currentIndexKey, currentIndex);
            PlayerPrefs.Save(); // 변경된 내용을 즉시 저장
            UpdateCostText();

            // 외부 객체 스프라이트 변경
            if (externalImageChanger != null)
            {
                externalImageChanger.ChangeSpriteToIndex(currentIndex); // 외부 객체의 스프라이트를 UI와 동기화
            }
        }
    }

    private void UpdateImage()
    {
        // UI 이미지 업데이트
        if (targetImage != null && currentIndex >= 0 && currentIndex < evolutionSprites.Length)
        {
            targetImage.sprite = evolutionSprites[currentIndex];
        }
    }

    private void UpdateCostText()
    {
        if (costText != null && currentIndex >= 0 && currentIndex < evolutionCosts.Length)
        {
            int nextCost = currentIndex < evolutionCosts.Length - 1 ? evolutionCosts[currentIndex] : 0;
            costText.text = nextCost > 0 ? nextCost.ToString() : "0";
        }
    }

    public void ResetImage()
    {
        // UI 초기화
        currentIndex = 0;
        UpdateImage();
        PlayerPrefs.SetInt(currentIndexKey, currentIndex);
        PlayerPrefs.Save(); // 변경된 내용을 즉시 저장
        swordbuy.ResetMoney(200000);
        UpdateCostText();

        // 외부 객체 초기화도 함께 호출
        if (externalImageChanger != null)
        {
            externalImageChanger.ResetSprite(); // 외부 객체도 초기화
        }
    }
}
