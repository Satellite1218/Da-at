using UnityEngine;
using UnityEngine.UI;

public class ImageChange : MonoBehaviour
{
    public Image targetImage;
    public Sprite[] evolutionSprites;
    private int currentIndex = 0;
    public int[] evolutionCosts;
    public Swordbuy swordbuy;
    public Text costText;

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

        if (swordbuy.SpendMoney(cost))
        {
            currentIndex = Mathf.Min(currentIndex + 1, evolutionSprites.Length - 1);
            UpdateImage();

            // 객체별로 고유한 키를 사용하여 현재 인덱스를 저장
            PlayerPrefs.SetInt(currentIndexKey, currentIndex);
            PlayerPrefs.Save(); // 변경된 내용을 즉시 저장
            UpdateCostText();
        }
    }

    private void UpdateImage()
    {
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
        currentIndex = 0;
        UpdateImage();
        PlayerPrefs.SetInt(currentIndexKey, currentIndex);
        PlayerPrefs.Save(); // 변경된 내용을 즉시 저장
        swordbuy.ResetMoney(200000);
        UpdateCostText();
    }
}
