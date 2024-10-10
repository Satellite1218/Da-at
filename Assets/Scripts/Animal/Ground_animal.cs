using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_animal : MonoBehaviour
{
    public float moveSpeed = 1f; // �̵� �ӵ� ����
    private Transform playerTransform;
    Animator anim;
    private bool isRunning = false; // �ڷ�ƾ �ߺ� ���� �÷���

    // �̵��ϴ� �ڷ�ƾ �Լ�
    IEnumerator Run(float n, float m)
    {
        if (isRunning) yield break; // �̹� �̵� ���̸� ���� �� ��
        isRunning = true; // �ڷ�ƾ ���� ��

        float totalTime = 4f / moveSpeed; // �� �̵� �ð��� moveSpeed�� ���� ����
        float elapsedTime = 0f; // ��� �ð�
        Vector3 startPosition = transform.position; // ���� ��ġ
        Vector3 targetPosition = startPosition + Vector3.right * Random.Range(n, m); // ��ǥ ��ġ
        anim = GetComponent<Animator>();

        // �ִϸ��̼� "isrun"�� true�� ���� (�̵� ����)
        anim.SetBool("isrun", true);

        // ���� ������Ʈ�� localScale ��������
        Vector3 currentScale = transform.localScale;

        // ���⿡ ���� x�� ���� (ũ�� ����)
        if (n > 0) // ���������� �̵��� ��
        {
            transform.localScale = new Vector3(-Mathf.Abs(currentScale.x), currentScale.y, currentScale.z); // x�ุ ����
        }
        else // �������� �̵��� ��
        {
            transform.localScale = new Vector3(Mathf.Abs(currentScale.x), currentScale.y, currentScale.z); // x���� �����
        }

        while (elapsedTime < totalTime)
        {
            float t = elapsedTime / totalTime;
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // �̵��� ������ �ִϸ��̼� "isrun"�� false�� ����
        anim.SetBool("isrun", false);

        // ���� ��ġ ����
        transform.position = targetPosition;
        isRunning = false; // �̵� �Ϸ� �� �ٽ� ���� ����
    }

    void ChangeR()
    {
        if (!isRunning) // �̵� ���� �ƴ� ���� ����
        {
            transform.position = new Vector3(-3f, -3.4f, 0f);
            StartCoroutine(Run(10f, 25f));
        }
    }

    void ChangeL()
    {
        if (!isRunning) // �̵� ���� �ƴ� ���� ����
        {
            transform.position = new Vector3(77f, -3.4f, 0f);
            StartCoroutine(Run(-10f, -25f));
        }
    }

    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        playerTransform = playerObject.transform;
    }

    void Update()
    {
        if (playerTransform == null) return;

        float playerX = playerTransform.position.x;

        if (!isRunning) // �̵� ���� �ƴ� ���� ����
        {
            if (transform.position.x >= playerX && transform.position.x <= playerX + 5)
            {
                StartCoroutine(Run(30f, 35f));
            }
            else if (transform.position.x <= playerX - 3 && transform.position.x >= playerX - 7)
            {
                StartCoroutine(Run(-30f, -35f));
            }
            else if (transform.position.x < -10)
            {
                ChangeL();
            }
            else if (transform.position.x > 45)
            {
                ChangeR();
            }
        }
    }
}
