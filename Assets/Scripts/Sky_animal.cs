using System.Collections;
using UnityEngine;

public class Sky_Animal : MonoBehaviour
{
    private Rigidbody2D _rb;
    private bool _isCoroutineRunning;
    private float _n;
    public Collider2D Area;
    private SpriteRenderer _spriteRenderer; // Add a reference to SpriteRenderer

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component

        if (_spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer�� �Ҵ���� �ʾҽ��ϴ�. GameObject�� SpriteRenderer�� �ִ��� Ȯ���ϼ���.");
        }

        // "Area"��� �̸��� ������Ʈ�� ã�� Collider2D�� �Ҵ�
        GameObject areaObject = GameObject.Find("Area");
        if (areaObject != null)
        {
            Area = areaObject.GetComponent<Collider2D>();
            if (Area == null)
            {
                Debug.LogWarning("Area ������Ʈ�� Collider2D ������Ʈ�� �����ϴ�.");
            }
        }
        else
        {
            Debug.LogWarning("�̸��� 'Area'�� ������Ʈ�� ã�� �� �����ϴ�.");
        }
    }

    // ĳ���͸� ��½�Ű�� �ڷ�ƾ
    IEnumerator Up()
    {
        // �� �����Ӹ��� Rigidbody2D�� Linear Drag ���� 3.2 ~ 4 ������ ���������� ����
        _rb.drag = Random.Range(3.2f, 4.0f);

        if (_isCoroutineRunning) yield break;
        _isCoroutineRunning = true;

        float totalTime = 2f;  // ��¿� �ɸ��� �� �ð�
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        // ��ǥ ��ġ ����
        float targetY = Mathf.Clamp(transform.position.y + Random.Range(2f, 5f), 5f, 6f);
        Vector3 targetPosition = new Vector3(transform.position.x, targetY, startPosition.z);

        // SmoothStep�� ����Ͽ� �ε巴�� ��ǥ ��ġ�� �̵�
        while (elapsedTime < totalTime)
        {
            float t = elapsedTime / totalTime;
            float smoothedT = Mathf.SmoothStep(0, 1, t);
            transform.position = new Vector3(
                transform.position.x,  // X�� �̵��� ����
                Mathf.Lerp(startPosition.y, targetPosition.y, smoothedT),
                startPosition.z);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        _isCoroutineRunning = false;

        // �����ϰ� Glide �ڷ�ƾ ����
        if (Random.Range(1, 3) == 1)
        {
            _n = Random.Range(-1f, 0f); // �������� Ȱ��
            StartCoroutine(Glide(Vector2.left));
        }
        else
        {
            _n = Random.Range(0f, 1f); // ���������� Ȱ��
            StartCoroutine(Glide(Vector2.right));
        }
    }

    // ĳ���͸� Ư�� �������� Ȱ����Ű�� ���� �ڷ�ƾ
    IEnumerator Glide(Vector2 direction)
    {
        if (_isCoroutineRunning) yield break;
        _isCoroutineRunning = true;

        float totalTime = Random.Range(1.5f, 2f);  // Ȱ���� �ɸ��� �� �ð�
        float elapsedTime = 0f;

        // �ʱ� �ӵ��� ��ǥ �ӵ� ����
        Vector2 initialVelocity = Vector2.zero;
        Vector2 targetVelocity = direction * Random.Range(5f, 8f);

        // ��������Ʈ�� ���⿡ ���� ����
        if (direction == Vector2.left)
        {
            transform.localScale = new Vector3(2.5f, 2.5f, 1); // ����
        }
        else if (direction == Vector2.right)
        {
            transform.localScale = new Vector3(-2.5f, 2.5f, 1); // ����
        }

        // SmoothDamp�� ����Ͽ� �ӵ��� õõ�� ������Ŵ
        while (elapsedTime < totalTime)
        {
            Vector2 currentVelocity = Vector2.SmoothDamp(_rb.velocity, targetVelocity, ref initialVelocity, totalTime);
            _rb.velocity = currentVelocity;
            transform.position += (Vector3)(direction * currentVelocity.magnitude * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _isCoroutineRunning = false;

        // ��� �ڷ�ƾ �ٽ� ����
        StartCoroutine(Up());
    }

    void Update()
    {

        if (!_isCoroutineRunning)
        {
            if (transform.position.y <= 0f)
            {
                StartCoroutine(Up());
            }
        }

        if (Area == null)
        {
            return;
        }

        if (!Area.bounds.Contains(transform.position))
        {
            Vector3 newPosition = transform.position;

            if (transform.position.x > Area.bounds.max.x)
            {
                newPosition.x = Area.bounds.min.x;
            }
            else if (transform.position.x < Area.bounds.min.x)
            {
                newPosition.x = Area.bounds.max.x;
            }

            if (transform.position.y > Area.bounds.max.y)
            {
                newPosition.y = Area.bounds.min.y;
            }
            else if (transform.position.y < Area.bounds.min.y)
            {
                newPosition.y = Area.bounds.max.y;
            }

            transform.position = newPosition;
        }
    }

}
