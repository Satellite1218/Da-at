using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // ī�޶� ���� Ÿ��
    public HidePlayerOnCollision HiPl;  // �÷��̾���� �浹�� ����� ���� ��ũ��Ʈ (������ ����)

    public float smoothSpeed = 3;  // ī�޶� �ε巴�� �̵��ϴ� �ӵ�
    public Vector2 offset;  // ī�޶�� Ÿ�� ���� ������ (X, Y)
    public float limitMinX, limitMaxX, limitMinY, limitMaxY;  // ī�޶� �̵� ���� �� (X, Y ��ǥ�� �ּ� �� �ִ밪)
    float cameraHalfWidth, cameraHalfHeight;  // ī�޶� �� �ʺ�� �� ����

    private bool isDragging = false;  // �巡�� ������ ���θ� ��Ÿ���� �÷���
    private Vector3 dragOrigin;  // �巡�� ���� ����
    private float zoomSpeed = 1.0f;  // �� �ӵ�
    private float originalOrthographicSize;  // ī�޶��� �ʱ� ���� ũ��
    private float maxZoomOutDistance = 10.0f;  // �ִ� �� �ƿ� �Ÿ�
    private float zoomFactor = 1.0f;  // �� �ƿ��� ���� ���� (�Ÿ� ���)

    private void Start()
    {
        // ī�޶��� �� �ʺ�� �� ���̸� ���
        cameraHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
        cameraHalfHeight = Camera.main.orthographicSize;
        // �ʱ� ī�޶� ���� ũ�⸦ ����
        originalOrthographicSize = Camera.main.orthographicSize;
    }

    private void LateUpdate()
    {
        // ���콺 ��ư�� ������ �巡�� ����
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            dragOrigin = Input.mousePosition;  // �巡�� ���� ��ġ ����
        }
        // ���콺 ��ư�� ���� �巡�� ���� �� �� ����
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            ResetZoom();  // ���� �ʱ� ������ ����
        }

        // �巡�� ���� ��
        if (isDragging)
        {
            // ���콺�� Y�� �̵����� ����Ͽ� �� �ӵ��� ���� ���� �ܿ� �ݿ�
            float deltaY = (Input.mousePosition.y - dragOrigin.y) * zoomSpeed * Time.deltaTime;

            // deltaY�� ���� ī�޶� �� ����
            float deltaZoom = deltaY;
            Camera.main.orthographicSize -= deltaZoom;  // ī�޶� ���� ũ�� ����

            // �巡�� ������ ������Ʈ
            dragOrigin = Input.mousePosition;
        }
        else
        {
            // Ÿ�ٰ� ī�޶� ���� �Ÿ� ���
            float distance = Vector2.Distance(target.position, transform.position);

            // �Ÿ� ������� �� ���� ���
            zoomFactor = Mathf.Clamp(distance / maxZoomOutDistance, 1.0f, Mathf.Infinity);

            // �� ���͸� ī�޶� ���� ũ�⿡ ����
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, originalOrthographicSize * zoomFactor, Time.deltaTime * smoothSpeed);

            // Ÿ�� ��ġ�� ���� ī�޶��� ��ǥ ��ġ�� ���
            Vector3 desiredPosition = new Vector3(
                Mathf.Clamp(target.position.x + offset.x, limitMinX + cameraHalfWidth, limitMaxX - cameraHalfWidth),   // X ��ǥ ����
                Mathf.Clamp(target.position.y + offset.y, limitMinY + cameraHalfHeight, limitMaxY - cameraHalfHeight), // Y ��ǥ ����
                -10);  // Z ��ǥ ���� (2D ī�޶� ��ġ)

            // ī�޶� ��ǥ ��ġ�� �ε巴�� �̵�
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
        }
    }

    private void ResetZoom()
    {
        // ���� �ʱ� ������ �ε巴�� ���� (���� ���)
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, originalOrthographicSize, Time.deltaTime * smoothSpeed);
    }
}
