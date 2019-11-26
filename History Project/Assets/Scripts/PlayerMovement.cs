using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerSpeed = 5f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float crouchSpeed = 3f;
    [SerializeField] float upstairsSpeed = 3f;
    [SerializeField] float jumpTime = 0.3f;
    [SerializeField] Collider2D standCollider;
    [SerializeField] Collider2D crouchCollider;
    [SerializeField] Transform feetPos;
    [SerializeField] float feetRadius = 0.5f;

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer sprite;
    Transform body;
    //Collider2D myCollider;

    float axisX;
    float axisY;
    float timeSinceJump = 0f;
    float xSpeed;
    float ySpeed;
    float gravity;

    bool isJumping;
    bool canJump=true;
    bool isGroundTouched;
    bool isCrouched;
    bool canGoUp=false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        body = transform.Find("BodyStructure");
        sprite = GetComponent<SpriteRenderer>();
        //myCollider = GetComponent<BoxCollider2D>();
        xSpeed = playerSpeed;
        gravity = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        axisX = Input.GetAxisRaw("Horizontal");

        FlipBody();

        StaircaseMovement();

        Jump();

        Crouch();

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(xSpeed * axisX, ySpeed * axisY);

        animator.SetFloat("SpeedX", axisX);
        animator.SetFloat("SpeedY", axisY);
    }

    private void Jump()
    {
        if (!canJump)
            return;

        if (standCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            axisY = 0f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isJumping = true;
            }
        }

        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            ySpeed = jumpSpeed;
            axisY = 1;
            timeSinceJump += Time.deltaTime;
            if (timeSinceJump > jumpTime)
            {
                axisY = -1;
                isJumping = false;
                timeSinceJump = 0f;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
            axisY = -1;
        }
    }
    private void StaircaseMovement()
    {
        if (isCrouched)
            return;

        if (standCollider.IsTouchingLayers(LayerMask.GetMask("Stairs")))
        {
            axisY = Input.GetAxisRaw("Vertical");
            canJump = false;
            rb.gravityScale = 0f;
            ySpeed = upstairsSpeed;
        }
        else
        {
            canJump = true;
            rb.gravityScale = gravity;
            ySpeed = 0f;
        }
    }

    private void FlipBody()
    {
        if (axisX == -1)
        {
            body.localScale = new Vector3(-1, body.localScale.y, body.localScale.z);
            sprite.flipX = true;
        }
        else if (axisX == 1)
        {
            body.localScale = new Vector3(1, body.localScale.y, body.localScale.z);
            sprite.flipX = false;
        }

    }

    private void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!isCrouched)
            {
                sprite.enabled = false;
                standCollider.enabled = false;
                crouchCollider.enabled = true;
                body.gameObject.SetActive(true);
                canJump = false;
                animator.SetBool("isCrouched", true);
                xSpeed = crouchSpeed;
                isCrouched = true;
            }
            else
            {
                sprite.enabled = true;
                crouchCollider.enabled = false;
                standCollider.enabled = true;
                body.gameObject.SetActive(false);
                canJump = true;
                animator.SetBool("isCrouched", false);
                xSpeed = playerSpeed;
                isCrouched = false;
            }
        }
    }
}
