using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float jumpTime = 0.3f;
    [SerializeField] Transform feetPos;
    [SerializeField] float feetRadius = 0.5f;

    Rigidbody2D rb;
    Animator animator;

    float axisX;
    float axisY;
    float timeSinceJump = 0f;

    bool isJumping;
    bool isGroundTouched;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        axisX = Input.GetAxisRaw("Horizontal");
        //axisY = Input.GetAxisRaw("Vertical");

        Jump();
    }

    private void Jump()
    {
        isGroundTouched = Physics2D.OverlapCircle(feetPos.position, feetRadius, LayerMask.GetMask("Ground"));

        if (isGroundTouched)
        {
            axisY = 0f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isJumping = true;
            }
        }

        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            axisY = 1f;
            timeSinceJump += Time.deltaTime;
            if (timeSinceJump > jumpTime)
            {
                axisY = -1f;
                isJumping = false;
                timeSinceJump = 0f;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
            axisY = -1f;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * axisX, jumpSpeed * axisY);

        animator.SetFloat("SpeedX", axisX);
        animator.SetFloat("SpeedY", axisY);
    }
}
