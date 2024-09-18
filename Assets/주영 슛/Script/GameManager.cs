using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton class: GameManager

    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    #endregion

    Camera cam;

    public Ball ball;
    public Trajectory trajectory;
    public GameObject gun; // 총 객체 추가
    [SerializeField] float pushForce = 4f;

    // 드래그 가능한 범위를 정의하는 변수
    [SerializeField] Rect draggableArea = new Rect(-5f, -5f, 10f, 10f);

    // Player 오브젝트 참조 추가
    public GameObject player;

    bool isDragging = false;

    Vector2 startPoint;
    Vector2 endPoint;
    Vector2 direction;
    Vector2 force;
    float distance;

    // 최대 드래그 거리를 나타내는 변수 (이 값을 조정하면 드래그 가능한 범위가 변경됨)
    public float maxDragDistance = 3f;

    //---------------------------------------
    void Start()
    {
        cam = Camera.main;
        ball.DeactivateRb(); // 공을 시작할 때만 비활성화
    }

    void Update()
    {
        draggableArea.x = player.transform.position.x + 1.5f - draggableArea.width / 2;

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            if (draggableArea.Contains(mousePosition))
            {
                isDragging = true;
                OnDragStart();
            }
        }
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            OnDragEnd();
        }

        if (isDragging)
        {
            OnDrag();
        }

        // 드래그 중에도 총 객체의 방향을 업데이트
        UpdateGunRotationDuringDrag();
    }

    //-Drag--------------------------------------
    void OnDragStart()
    {
        ball.DeactivateRb(); // 드래그 시작할 때 공을 비활성화
        startPoint = cam.ScreenToWorldPoint(Input.mousePosition);

        trajectory.Show();
    }

    void OnDrag()
    {
        endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        distance = Vector2.Distance(startPoint, endPoint);

        // 드래그 거리가 maxDragDistance를 초과하지 않도록 제한
        if (distance > maxDragDistance)
        {
            distance = maxDragDistance;
            endPoint = startPoint - direction.normalized * maxDragDistance;
        }

        direction = (startPoint - endPoint).normalized;
        force = direction * distance * pushForce;

        trajectory.UpdateDots(ball.pos, force);
    }

    void OnDragEnd()
    {
        // 공을 밀기
        ball.ActivateRb();
        ball.Push(force);

        trajectory.Hide();
    }

    void UpdateGunRotationDuringDrag()
    {
        // 공의 위치를 가져와 총이 바라보게 합니다.
        Vector2 gunDirection = (Vector2)ball.transform.position - (Vector2)gun.transform.position;
        float angle = Mathf.Atan2(gunDirection.y, gunDirection.x) * Mathf.Rad2Deg;
        gun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    //-----------------------------------------
    public void ReloadBall()
    {
        ball.DeactivateRb();
        ball.transform.position = Vector3.zero;
        trajectory.Show();
    }

    // Gizmos 그리기
    void OnDrawGizmos()
    {
        // Draggable area 표시 (초록색 사각형)
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(draggableArea.Center(), draggableArea.Size());

        // 최대 드래그 거리 표시 (빨간색 원)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(ball.transform.position, maxDragDistance);  // 원점에서 maxDragDistance 반경으로 원 그리기
    }
}

public static class RectExtensions
{
    public static Vector2 Center(this Rect rect)
    {
        return new Vector2(rect.x + rect.width / 2, rect.y + rect.height / 2);
    }

    public static Vector2 Size(this Rect rect)
    {
        return new Vector2(rect.width, rect.height);
    }
}
