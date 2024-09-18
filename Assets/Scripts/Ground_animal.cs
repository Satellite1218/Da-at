using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_animal : MonoBehaviour
{
    public float moveSpeed;
    private Transform playerTransform;
    Animator anim;

    // 이동하는 코루틴 함수
    IEnumerator Run(float n, float m)
    {
        float totalTime = 4f; // 총 이동 시간
        float elapsedTime = 0f; // 경과 시간
        Vector3 startPosition = transform.position; // 시작 위치
        Vector3 targetPosition = startPosition + Vector3.right * Random.Range(n, m); // 목표 위치
        anim = GetComponent<Animator>();

        // 애니메이션 "isrun"을 true로 설정 (이동 시작)
        anim.SetBool("isrun", true);
        transform.localScale = new Vector3(-5.5f, 5.5f, 1);

        while (elapsedTime < totalTime)
        {
            float t = elapsedTime / totalTime;
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 이동이 끝나면 애니메이션 "isrun"을 false로 설정
        anim.SetBool("isrun", false);
        transform.localScale = new Vector3(5.5f, 5.5f, 1);

        // 최종 위치 설정
        transform.position = targetPosition;
    }

    void ChangeR()
    {
        transform.position = new Vector3(-3f, -3.4f, 0f);
        StartCoroutine(Run(10f, 25f));
    }

    void ChangeL()
    {
        transform.position = new Vector3(77f, -3.4f, 0f);
        StartCoroutine(Run(-10f, -25f));
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
