using UnityEngine;
using UnityEngine.UI;

public class Swordbuy : MonoBehaviour
{
    // 플레이어의 돈을 표시할 텍스트 UI
    public Text moneyText;

    // 플레이어의 현재 돈
    private int currentMoney;

    private const string CurrentMoneyKey = "CurrentMoney";

    void Start()
    {
        // 저장된 돈을 불러와서 설정
        currentMoney = PlayerPrefs.GetInt(CurrentMoneyKey, 300000); // 초기 돈 설정 (1000으로 가정)
        UpdateMoneyText();
    }

    // 돈을 차감하는 함수
    public bool SpendMoney(int amount)
    {
        if (currentMoney >= amount)
        {
            currentMoney -= amount;
            PlayerPrefs.SetInt(CurrentMoneyKey, currentMoney);
            UpdateMoneyText();
            return true;
        }
        else
        {
            Debug.Log("Not enough money.");
            return false;
        }
    }

    // 돈 UI를 업데이트하는 함수
    private void UpdateMoneyText()
    {
        if (moneyText != null)
        {
            moneyText.text = "Money: " + currentMoney.ToString();
        }
    }

    // 돈을 초기화하는 함수
    public void ResetMoney(int initialMoney)
    {
        currentMoney = initialMoney;
        PlayerPrefs.SetInt(CurrentMoneyKey, currentMoney);
        UpdateMoneyText();
    }
}
