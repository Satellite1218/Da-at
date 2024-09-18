using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // 카메라가 따라갈 타겟
    public HidePlayerOnCollision HiPl;  // 플레이어와의 충돌을 숨기기 위한 스크립트 (사용되지 않음)

    public float smoothSpeed = 3;  // 카메라가 부드럽게 이동하는 속도
    public Vector2 offset;  // 카메라와 타겟 간의 오프셋 (X, Y)
    public float limitMinX, limitMaxX, limitMinY, limitMaxY;  // 카메라 이동 제한 값 (X, Y 좌표의 최소 및 최대값)
    float cameraHalfWidth, cameraHalfHeight;  // 카메라 반 너비와 반 높이

    private bool isDragging = false;  // 드래그 중인지 여부를 나타내는 플래그
    private Vector3 dragOrigin;  // 드래그 시작 지점
    private float zoomSpeed = 1.0f;  // 줌 속도
    private float originalOrthographicSize;  // 카메라의 초기 직교 크기
    private float maxZoomOutDistance = 10.0f;  // 최대 줌 아웃 거리
    private float zoomFactor = 1.0f;  // 줌 아웃에 따른 팩터 (거리 비례)

    private void Start()
    {
        // 카메라의 반 너비와 반 높이를 계산
        cameraHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
        cameraHalfHeight = Camera.main.orthographicSize;
        // 초기 카메라 직교 크기를 저장
        originalOrthographicSize = Camera.main.orthographicSize;
    }

    private void LateUpdate()
    {
        // 마우스 버튼을 누르면 드래그 시작
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            dragOrigin = Input.mousePosition;  // 드래그 시작 위치 저장
        }
        // 마우스 버튼을 떼면 드래그 중지 및 줌 리셋
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            ResetZoom();  // 줌을 초기 값으로 리셋
        }

        // 드래그 중일 때
        if (isDragging)
        {
            // 마우스의 Y축 이동량을 계산하여 줌 속도에 곱한 값을 줌에 반영
            float deltaY = (Input.mousePosition.y - dragOrigin.y) * zoomSpeed * Time.deltaTime;

            // deltaY를 통해 카메라 줌 조정
            float deltaZoom = deltaY;
            Camera.main.orthographicSize -= deltaZoom;  // 카메라 직교 크기 조정

            // 드래그 원점을 업데이트
            dragOrigin = Input.mousePosition;
        }
        else
        {
            // 타겟과 카메라 간의 거리 계산
            float distance = Vector2.Distance(target.position, transform.position);

            // 거리 기반으로 줌 팩터 계산
            zoomFactor = Mathf.Clamp(distance / maxZoomOutDistance, 1.0f, Mathf.Infinity);

            // 줌 팩터를 카메라 직교 크기에 적용
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, originalOrthographicSize * zoomFactor, Time.deltaTime * smoothSpeed);

            // 타겟 위치에 따라 카메라의 목표 위치를 계산
            Vector3 desiredPosition = new Vector3(
                Mathf.Clamp(target.position.x + offset.x, limitMinX + cameraHalfWidth, limitMaxX - cameraHalfWidth),   // X 좌표 제한
                Mathf.Clamp(target.position.y + offset.y, limitMinY + cameraHalfHeight, limitMaxY - cameraHalfHeight), // Y 좌표 제한
                -10);  // Z 좌표 고정 (2D 카메라 위치)

            // 카메라를 목표 위치로 부드럽게 이동
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
        }
    }

    private void ResetZoom()
    {
        // 줌을 초기 값으로 부드럽게 리셋 (보간 사용)
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, originalOrthographicSize, Time.deltaTime * smoothSpeed);
    }
}
