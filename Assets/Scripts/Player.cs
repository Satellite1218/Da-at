using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
        float speed = Time.deltaTime * 5f;

        if (Input.GetKey(KeyCode.LeftArrow) && MoveBack)
        {
            anim.SetBool("Isrunning", true);
            transform.Translate(-Vector2.right * speed);
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
        transform.position = new Vector3(-10f, -2.2f, 0);
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
        else if (transform.position.x > 45)
        {
            ChangeR();
        }
        else
        {
            Move();
        }
    }

    public void GoStop()
    {
        MoveForward = false; // ������ �̵� �Ұ� ����
    }
    public void BackStop()
    {
        MoveBack = false; // �ڷ� �̵� �Ұ� ����
    }
    public void Go()
    {
        MoveForward = true; // ������ �̵� �Ұ� ����
        MoveBack = true; // �ڷ� �̵� �Ұ� ����
    }
}
