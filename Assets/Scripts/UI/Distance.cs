using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI ���ӽ����̽� ����

public class Distance : MonoBehaviour
{
    public GameObject targetObject; // �Ÿ� ����� ��� ������Ʈ
    private Vector3 previousPosition; // ���� ��ġ ����
    public float totalDistance; // �� �̵� �Ÿ� ���� (public���� ����)
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

        // ���� ��ġ�� ���� ��ġ ������ ��ü �Ÿ� ��� (x, y, z �� ��� ����)
        float distanceMoved = Vector3.Distance(targetObject.transform.position, previousPosition);

        // �̵��� �Ÿ��� �� �̵� �Ÿ��� �߰�
        totalDistance += distanceMoved;

        // ���� ��ġ ������Ʈ
        previousPosition = targetObject.transform.position;

        // �� �̵� �Ÿ��� ������ �ݿø��Ͽ� UI Text�� ������Ʈ
        distanceText.text = Mathf.RoundToInt(totalDistance) + "m";

        // �� �̵� �Ÿ��� 200 �̻��̸� ������ ���� ���� ����
        if (totalDistance >= 200)
        {
            playerScript.GoStop();
            distanceText.text = "�Ÿ��ʰ�"; // UI�� �̵� ���� ǥ��
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
