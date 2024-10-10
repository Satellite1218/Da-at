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

        // ���� ��ġ�� ���� ��ġ ������ �Ÿ� ��� (x�ุ)
        float distanceMoved = targetObject.transform.position.x - previousPosition.x;

        // ���������� �̵� ���̸� �Ÿ��� ���ϰ�, �������� �̵� ���̸� �Ÿ��� ���ҽ�Ŵ
        if (distanceMoved > 0)
        {
            totalDistance += distanceMoved; // ������ �̵� -> �Ÿ� ����
        }
        else if (distanceMoved < 0)
        {
            totalDistance += distanceMoved; // ���� �̵� -> �Ÿ� ����
        }

        // ���� ��ġ ������Ʈ
        previousPosition = targetObject.transform.position;

        // �� �̵� �Ÿ��� ������ �ݿø��Ͽ� UI Text�� ������Ʈ
        distanceText.text = "�̵� �Ÿ� : " + Mathf.RoundToInt(totalDistance) + "m";

        // �� �̵� �Ÿ��� 70 �̻��̸� ������ ���� ���� ����
        if (totalDistance >= 200)
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
