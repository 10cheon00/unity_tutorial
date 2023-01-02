using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const float DodgeFinishTime = 1f;
    private float speed = 0.25f;
    private float jumpPower = 10f;
    private float hAxis;
    private float vAxis;
    private bool isRunning = false;

    [SerializeField]
    private bool isDodging = false;
    private bool isJumping = false;
    private bool isJumpKeyDown = false;
    private bool isDodgeKeyDown = false;
    private Vector3 moveVec;
    private Animator animator;
    private Rigidbody rigid;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetInput();
        Jump();
        Dodge();
    }

    void GetInput()
    {
        if (isJumping == false && isDodging == false)
        {
            hAxis = Input.GetAxisRaw("Horizontal");
            vAxis = Input.GetAxisRaw("Vertical");
        }
        isJumpKeyDown = Input.GetKeyDown("space");
        isDodgeKeyDown = Input.GetKeyDown("c");
    }

    void Jump()
    {
        if (isJumpKeyDown && isJumping == false)
        {
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            animator.SetTrigger("DoJump");
            isJumping = true;
        }
    }

    void Dodge()
    {
        if (isDodgeKeyDown && isDodging == false)
        {
            Invoke("FinishDodge", DodgeFinishTime);
            animator.SetTrigger("DoDodge");
            isDodging = true;
        }
    }

    void FinishDodge()
    {
        isDodging = false;
    }

    void FixedUpdate()
    {
        Move();
        SetAnimatorValue();
        LookForDirection();
    }

    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized * speed;
        if (isRunning)
        {
            moveVec *= 2;
        }
        transform.position += moveVec;
    }

    void SetAnimatorValue()
    {
        animator.SetBool("IsWalking", moveVec != Vector3.zero);
        isRunning = Input.GetButton("Run");
        animator.SetBool("IsRunning", isRunning);
        animator.SetBool("IsJumping", isJumping);
    }

    void LookForDirection()
    {
        transform.LookAt(transform.position + moveVec, Vector3.up);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "ground")
        {
            isJumping = false;
        }
    }
}
