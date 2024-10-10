using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI 네임스페이스 포함

public class Distance : MonoBehaviour
{
    public GameObject targetObject; // 거리 계산할 대상 오브젝트
    private Vector3 previousPosition; // 이전 위치 저장
    private float totalDistance; // 총 이동 거리 저장
    private Text distanceText; // UI Text 컴포넌트 참조
    private player playerScript; // player 스크립트 참조

    // Start는 첫 프레임 업데이트 전에 호출됩니다.
    void Start()
    {
        if (targetObject == null)
        {
            Debug.LogError("Target Object가 설정되지 않았습니다.");
            return;
        }

        distanceText = GetComponent<Text>(); // UI Text 컴포넌트 가져오기
        previousPosition = targetObject.transform.position; // 이전 위치 초기화
        totalDistance = 0f; // 총 거리 초기화
        playerScript = targetObject.GetComponent<player>(); // player 스크립트 컴포넌트 가져오기
    }

    // Update는 매 프레임 호출됩니다.
    void Update()
    {
        if (targetObject == null || playerScript == null) return;

        // 현재 위치와 이전 위치 사이의 거리 계산 (x축만)
        float distanceMoved = targetObject.transform.position.x - previousPosition.x;

        // 오른쪽으로 이동 중이면 거리를 더하고, 왼쪽으로 이동 중이면 거리를 감소시킴
        if (distanceMoved > 0)
        {
            totalDistance += distanceMoved; // 오른쪽 이동 -> 거리 증가
        }
        else if (distanceMoved < 0)
        {
            totalDistance += distanceMoved; // 왼쪽 이동 -> 거리 감소
        }

        // 이전 위치 업데이트
        previousPosition = targetObject.transform.position;

        // 총 이동 거리를 정수로 반올림하여 UI Text에 업데이트
        distanceText.text = "이동 거리 : " + Mathf.RoundToInt(totalDistance) + "m";

        // 총 이동 거리가 70 이상이면 앞으로 가는 것을 멈춤
        if (totalDistance >= 200)
        {
            playerScript.GoStop();
            distanceText.text = "이동 거리 : 거리초과"; // UI에 이동 멈춤 표시
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
