using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI ���ӽ����̽� ����

public class Distance : MonoBehaviour
{
    public GameObject targetObject; // �Ÿ� ����� ��� ������Ʈ
    private Vector3 previousPosition; // ���� ��ġ ����
    private float totalDistance; // �� �̵� �Ÿ� ����
    private Text distanceText; // UI Text ������Ʈ ����
    private player playerScript; // player ��ũ��Ʈ ����

    // Start�� ù ������ ������Ʈ ���� ȣ��˴ϴ�.
    void Start()
    {
        if (targetObject == null)
        {
            Debug.LogError("Target Object�� �������� �ʾҽ��ϴ�.");
            return;
        }

        distanceText = GetComponent<Text>(); // UI Text ������Ʈ ��������
        previousPosition = targetObject.transform.position; // ���� ��ġ �ʱ�ȭ
        totalDistance = 0f; // �� �Ÿ� �ʱ�ȭ
        playerScript = targetObject.GetComponent<player>(); // player ��ũ��Ʈ ������Ʈ ��������
    }

    // Update�� �� ������ ȣ��˴ϴ�.
    void Update()
    {
        if (targetObject == null || playerScript == null) return;

        // ���� ������ ���� �̵��� �Ÿ� ���
        float distanceMoved = targetObject.transform.position.x - previousPosition.x;

        // �̵��� �Ÿ��� �� �̵� �Ÿ��� ����
        totalDistance += distanceMoved;

        // ���� ��ġ ������Ʈ
        previousPosition = targetObject.transform.position;

        // �� �̵� �Ÿ��� ������ �ݿø��Ͽ� UI Text�� ������Ʈ
        distanceText.text = "�̵� �Ÿ� : " + Mathf.RoundToInt(totalDistance) / 2 + "m";

        // �� �̵� �Ÿ��� 70 �̻��̸� ������ ���� ���� ����
        if (totalDistance >= 70)
        {
            playerScript.GoStop();
            distanceText.text = "�̵� �Ÿ� : �Ÿ��ʰ�"; // UI�� �̵� ���� ǥ��
        }
        else if (totalDistance < 0)
        {
            playerScript.BackStop();
        }
        else
        {
            playerScript.Go();
        }
    }
}