using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class player : MonoBehaviour
{
    private bool MoveForward = true; // 앞으로 이동 가능 여부
    private bool MoveBack = true; // 뒤로 이동 가능 여부
    Animator anim;
        
    // Start는 첫 프레임 업데이트 전에 호출됩니다.
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

    // Update는 매 프레임 호출됩니다.
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
        MoveForward = false; // 앞으로 이동 불가 설정
    }
    public void BackStop()
    {
        MoveBack = false; // 뒤로 이동 불가 설정
    }
    public void Go()
    {
        MoveForward = true; // 앞으로 이동 불가 해제
        MoveBack = true; // 뒤로 이동 불가 해제
    }
}
