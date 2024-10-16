using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    private bool MoveForward = true; // ������ �̵� ���� ����
    private bool MoveBack = true; // �ڷ� �̵� ���� ����
    Animator anim;

    // Start�� ù ������ ������Ʈ ���� ȣ��˴ϴ�.
    private void Start()
    {
        anim = GetComponent<Animator>();
        transform.position = new Vector3(-5.5f, -2.2f, 0);
    }

    public void Move()
    {
        if (!MoveForward && !MoveBack) return; // �̵� ���� ���θ� üũ
        float speed = Time.deltaTime * 5f;

        if (Input.GetKey(KeyCode.LeftArrow) && MoveBack)
        {
            anim.SetBool("Isrunning", true);
            transform.Translate(-Vector2.right * speed / 2);
        }
        else if (Input.GetKey(KeyCode.RightArrow) && MoveForward)
        {
            anim.SetBool("Isrunning", true);
            transform.Translate(Vector2.right * speed);
        }
        else
        {
            anim.SetBool("Isrunning", false);
        }
    }

    void ChangeR()
    {
        transform.position = new Vector3(-5f, -2.2f, 0);
    }

    void ChangeL()
    {
        transform.position = new Vector3(45f, -2.2f, 0);
    }

    // Update�� �� ������ ȣ��˴ϴ�.
    void Update()
    {
        if (transform.position.x < -10)
        {
            ChangeL();
        }
        else if (transform.position.x > 80)
        {
            ChangeR();
        }
        else if (MoveForward != false && MoveBack != false)
        {
            Move();
        }
    }

    public void GoStop()
    {
        anim.SetBool("Isrunning", false);
        MoveForward = false; // ������ �̵� �Ұ� ����
    }
    public void BackStop()
    {
        anim.SetBool("Isrunning", false);
        MoveBack = false; // �ڷ� �̵� �Ұ� ����
    }
    public void Stop()
    {
        anim.SetBool("Isrunning", false);
        MoveForward = false; // ������ �̵� �Ұ� ����
        MoveBack = false; // �ڷ� �̵� �Ұ� ����
    }
    public void Go()
    {
        MoveForward = true; // ������ �̵� �Ұ� ����
        MoveBack = true; // �ڷ� �̵� �Ұ� ����
    }
}
