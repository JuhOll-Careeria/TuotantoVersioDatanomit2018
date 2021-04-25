using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoment : MonoBehaviour
{
    public Animator animator;
    public float speed = 10, jumpVelocity = 10;
    public LayerMask playerMask;
    float direction = 1;
    float normalGravity;

    bool isGrounded = false;
    bool isDashing;
    bool canDash = true;
    private bool facingRight;

    Rigidbody2D myBody;
    Transform myTrans, tagGround;
    float hInput = 0;
    IEnumerator dashCoroutine;


    void Start()
    {
        facingRight = true;
        myBody = this.GetComponent<Rigidbody2D>();
        myTrans = this.transform;
        tagGround = GameObject.Find(this.name + "/tag_ground").transform;
        normalGravity = myBody.gravityScale;
    }

    void Update()
    {
        

        isGrounded = Physics2D.Linecast(myTrans.position, tagGround.position, playerMask);

        if (hInput != 0)
        {
            direction = hInput;
        }

        Move(Input.GetAxisRaw("Horizontal"));
        if (Input.GetButtonDown("Jump"))
        Jump();
        Move(hInput);
        Flip(hInput);

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash == true)
        {
            if (dashCoroutine != null)
            {
            StopCoroutine(dashCoroutine);
            }
            dashCoroutine = Dash(.2f, 3);
            StartCoroutine(dashCoroutine);
            Dash();
        }

        if (myBody.velocity.y == 0)
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", false);
        }

        if(myBody.velocity.y > 0)
        {
            animator.SetBool("IsJumping", true);
        }

        if(myBody.velocity.y < 0)
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", true);
        }

        if(isDashing == true)
        {
            animator.SetBool("IsDashing", true);
        }

        else
        {
            animator.SetBool("IsDashing", false);
        }
        
    }
    private void FixedUpdate()
    {
        if (isDashing)
        {
            myBody.AddForce(new Vector2(direction * 60, 0), ForceMode2D.Impulse);
        }
    }

    void Move(float horizontalInput)
    {
        Vector2 moveVel = myBody.velocity;
        moveVel.x = horizontalInput * speed;
        myBody.velocity = moveVel;
    }

    public void Jump()
    {
        if (isGrounded)
        {
            animator.SetTrigger("TakeOff");
            myBody.velocity += jumpVelocity * Vector2.up;
        }

        else
        {
            animator.SetBool("IsJumping", true);
        }
    }

    public void StartMoving(float horizonalInput)
    {
        hInput = horizonalInput;
        animator.SetFloat("Speed", Mathf.Abs(hInput));
    }

    private void Flip(float hInput)
    {
        if (hInput > 0 && !facingRight || hInput < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;

            theScale.x *= -1;

            transform.localScale = theScale;
        }
    }

    public void Dash()
    {
        if(canDash == true)
        {
            if (dashCoroutine != null)
            {
                StopCoroutine(dashCoroutine);
            }
            dashCoroutine = Dash(.2f, 3);
            StartCoroutine(dashCoroutine);
        }
    }

    IEnumerator Dash(float dashDuration, float dashCooldown)
    {
        Vector2 originalVelocity = myBody.velocity;
        isDashing = true;
        canDash = false;
        myBody.gravityScale = 0;
        myBody.velocity = Vector2.zero;
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        myBody.gravityScale = normalGravity;
        myBody.velocity = originalVelocity;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
