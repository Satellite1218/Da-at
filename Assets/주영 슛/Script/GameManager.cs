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
    public GameObject gun; // �� ��ü �߰�
    [SerializeField] float pushForce = 4f;

    // �巡�� ������ ������ �����ϴ� ����
    [SerializeField] Rect draggableArea = new Rect(-5f, -5f, 10f, 10f);

    // Player ������Ʈ ���� �߰�
    public GameObject player;

    bool isDragging = false;

    Vector2 startPoint;
    Vector2 endPoint;
    Vector2 direction;
    Vector2 force;
    float distance;

    // �ִ� �巡�� �Ÿ��� ��Ÿ���� ���� (�� ���� �����ϸ� �巡�� ������ ������ �����)
    public float maxDragDistance = 3f;

    //---------------------------------------
    void Start()
    {
        cam = Camera.main;
        ball.DeactivateRb(); // ���� ������ ���� ��Ȱ��ȭ
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

        // �巡�� �߿��� �� ��ü�� ������ ������Ʈ
        UpdateGunRotationDuringDrag();
    }

    //-Drag--------------------------------------
    void OnDragStart()
    {
        ball.DeactivateRb(); // �巡�� ������ �� ���� ��Ȱ��ȭ
        startPoint = cam.ScreenToWorldPoint(Input.mousePosition);

        trajectory.Show();
    }

    void OnDrag()
    {
        endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        distance = Vector2.Distance(startPoint, endPoint);

        // �巡�� �Ÿ��� maxDragDistance�� �ʰ����� �ʵ��� ����
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
        // ���� �б�
        ball.ActivateRb();
        ball.Push(force);

        trajectory.Hide();
    }

    void UpdateGunRotationDuringDrag()
    {
        // ���� ��ġ�� ������ ���� �ٶ󺸰� �մϴ�.
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

    // Gizmos �׸���
    void OnDrawGizmos()
    {
        // Draggable area ǥ�� (�ʷϻ� �簢��)
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(draggableArea.Center(), draggableArea.Size());

        // �ִ� �巡�� �Ÿ� ǥ�� (������ ��)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(ball.transform.position, maxDragDistance);  // �������� maxDragDistance �ݰ����� �� �׸���
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
