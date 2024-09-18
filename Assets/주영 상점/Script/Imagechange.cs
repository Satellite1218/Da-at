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
        // ��ü���� ������ Ű ���� (��: ��ü �̸� ���)
        currentIndexKey = $"{gameObject.name}_CurrentImageIndex";

        // ����� �ε����� �ҷ��ͼ� ����
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

            // ��ü���� ������ Ű�� ����Ͽ� ���� �ε����� ����
            PlayerPrefs.SetInt(currentIndexKey, currentIndex);
            PlayerPrefs.Save(); // ����� ������ ��� ����
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
        PlayerPrefs.Save(); // ����� ������ ��� ����
        swordbuy.ResetMoney(200000);
        UpdateCostText();
    }
}
