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
    [SerializeField] Transform feetPos;
    [SerializeField] float feetRadius = 0.5f;

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer sprite;
    Transform body;

    float axisX;
    float axisY;
    float timeSinceJump = 0f;
    float xSpeed;
    float ySpeed;

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
        xSpeed = playerSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        axisX = Input.GetAxisRaw("Horizontal");
        axisY = Input.GetAxisRaw("Vertical");

        FlipBody();

        //if(axisY!=0 && canGoUp)
        //{
        //    ySpeed = upstairsSpeed;
        //}
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 1f, LayerMask.GetMask("Stairs"));
        if(hit)
        {
            ySpeed = upstairsSpeed;
        }

        Jump();

        if(Input.GetKeyDown(KeyCode.R))
        {
            Crouch();
        }
    }

    private void Jump()
    {
        isGroundTouched = Physics2D.OverlapCircle(feetPos.position, feetRadius, LayerMask.GetMask("Ground"));

        if (isGroundTouched)
        {
            jumpSpeed = 0;
            if (Input.GetKeyDown(KeyCode.Space) && canJump)
            {
                isJumping = true;
            }
        }

        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            ySpeed = jumpSpeed;
            timeSinceJump += Time.deltaTime;
            if (timeSinceJump > jumpTime)
            {
                ySpeed = -jumpSpeed;
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
        rb.velocity = new Vector2(xSpeed * axisX, ySpeed * axisY);
        //Debug.Log(rb.velocity);
        //Debug.Log("yspeed  "+ySpeed);
        //Debug.Log("axisY  "+axisY);

        animator.SetFloat("SpeedX", axisX);
        animator.SetFloat("SpeedY", axisY);
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
        if(!isCrouched)
        {
            sprite.enabled = false;
            body.gameObject.SetActive(true);
            canJump = false;
            animator.SetBool("isCrouch", true);
            xSpeed = crouchSpeed;
            isCrouched = true;
        }
        else
        {
            sprite.enabled = true;
            body.gameObject.SetActive(false);
            canJump = true;
            animator.SetBool("isCrouch", false);
            xSpeed = playerSpeed;
            isCrouched = false;
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.CompareTag("Stairs"))
    //    {
    //        canGoUp = true;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Stairs"))
    //    {
    //        canGoUp = false;
    //    }
    //}
}
