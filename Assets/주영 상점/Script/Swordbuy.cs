using UnityEngine;
using UnityEngine.UI;

public class Swordbuy : MonoBehaviour
{
    // �÷��̾��� ���� ǥ���� �ؽ�Ʈ UI
    public Text moneyText;

    // �÷��̾��� ���� ��
    private int currentMoney;

    private const string CurrentMoneyKey = "CurrentMoney";

    void Start()
    {
        // ����� ���� �ҷ��ͼ� ����
        currentMoney = PlayerPrefs.GetInt(CurrentMoneyKey, 300000); // �ʱ� �� ���� (1000���� ����)
        UpdateMoneyText();
    }

    // ���� �����ϴ� �Լ�
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

    // �� UI�� ������Ʈ�ϴ� �Լ�
    private void UpdateMoneyText()
    {
        if (moneyText != null)
        {
            moneyText.text = "Money: " + currentMoney.ToString();
        }
    }

    // ���� �ʱ�ȭ�ϴ� �Լ�
    public void ResetMoney(int initialMoney)
    {
        currentMoney = initialMoney;
        PlayerPrefs.SetInt(CurrentMoneyKey, currentMoney);
        UpdateMoneyText();
    }
}
