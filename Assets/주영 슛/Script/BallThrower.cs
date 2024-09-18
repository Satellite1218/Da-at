using UnityEngine;

public class BallThrower : MonoBehaviour
{
    public Ball ball; // Inspector에서 Ball GameObject를 드래그하세요.
    public Vector2 throwForce = new Vector2(10, 10);

    void Start()
    {
        // 초기에는 공을 비활성화합니다.
        ball.DeactivateRb();

        // 효과를 확인하기 위해 1초 후에 공을 던집니다.
        Invoke("ThrowBall", 1.0f);
    }

    void ThrowBall()
    {
        ball.ActivateRb();
        ball.Push(throwForce);
    }
}
