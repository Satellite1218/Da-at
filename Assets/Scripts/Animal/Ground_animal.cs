using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_animal : MonoBehaviour
{
    public float moveSpeed = 1f; // 이동 속도 설정
    private Transform playerTransform;
    Animator anim;
    private bool isRunning = false; // 코루틴 중복 방지 플래그

    // 이동하는 코루틴 함수
    IEnumerator Run(float n, float m)
    {
        if (isRunning) yield break; // 이미 이동 중이면 실행 안 함
        isRunning = true; // 코루틴 실행 중

        float totalTime = 4f / moveSpeed; // 총 이동 시간을 moveSpeed에 따라 조정
        float elapsedTime = 0f; // 경과 시간
        Vector3 startPosition = transform.position; // 시작 위치
        Vector3 targetPosition = startPosition + Vector3.right * Random.Range(n, m); // 목표 위치
        anim = GetComponent<Animator>();

        // 애니메이션 "isrun"을 true로 설정 (이동 시작)
        anim.SetBool("isrun", true);

        // 현재 오브젝트의 localScale 가져오기
        Vector3 currentScale = transform.localScale;

        // 방향에 따라 x축 반전 (크기 유지)
        if (n > 0) // 오른쪽으로 이동할 때
        {
            transform.localScale = new Vector3(-Mathf.Abs(currentScale.x), currentScale.y, currentScale.z); // x축만 반전
        }
        else // 왼쪽으로 이동할 때
        {
            transform.localScale = new Vector3(Mathf.Abs(currentScale.x), currentScale.y, currentScale.z); // x축을 양수로
        }

        while (elapsedTime < totalTime)
        {
            float t = elapsedTime / totalTime;
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 이동이 끝나면 애니메이션 "isrun"을 false로 설정
        anim.SetBool("isrun", false);

        // 최종 위치 설정
        transform.position = targetPosition;
        isRunning = false; // 이동 완료 후 다시 실행 가능
    }

    void ChangeR()
    {
        if (!isRunning) // 이동 중이 아닐 때만 실행
        {
            transform.position = new Vector3(-3f, -3.4f, 0f);
            StartCoroutine(Run(10f, 25f));
        }
    }

    void ChangeL()
    {
        if (!isRunning) // 이동 중이 아닐 때만 실행
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

        if (!isRunning) // 이동 중이 아닐 때만 실행
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
