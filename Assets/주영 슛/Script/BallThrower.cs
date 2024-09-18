using UnityEngine;

public class BallThrower : MonoBehaviour
{
    public Ball ball; // Inspector���� Ball GameObject�� �巡���ϼ���.
    public Vector2 throwForce = new Vector2(10, 10);

    void Start()
    {
        // �ʱ⿡�� ���� ��Ȱ��ȭ�մϴ�.
        ball.DeactivateRb();

        // ȿ���� Ȯ���ϱ� ���� 1�� �Ŀ� ���� �����ϴ�.
        Invoke("ThrowBall", 1.0f);
    }

    void ThrowBall()
    {
        ball.ActivateRb();
        ball.Push(throwForce);
    }
}
