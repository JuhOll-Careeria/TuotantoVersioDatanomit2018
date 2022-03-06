using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMoment : MonoBehaviour
{
    [SerializeField] private MobileHealthController healthController;
    [SerializeField] private float healthBag;

    public static PlayerMoment Instance = null;

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

    //LevelManager lm;
    LevelManager lm;
    //LevelLoader ll;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        //else if (Instance != this)
            //Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);

       //LevelManager.Instance.Player = this;
    }

    void Start()
    {
        facingRight = true;
        myBody = this.GetComponent<Rigidbody2D>();
        myTrans = this.transform;
        tagGround = GameObject.Find(this.name + "/tag_ground").transform;
        normalGravity = myBody.gravityScale;
        lm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
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
            SoundManager.PlaySound("Hyppy");
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

            transform.Rotate(0f, 180f, 0f);

            //Vector3 theScale = transform.localScale;

            //theScale.x *= -1;

            //transform.localScale = theScale;
        }
    }

    public void Dash()
    {
        if(canDash == true)
        {
            SoundManager.PlaySound("Dash");
            if (dashCoroutine != null)
            {
                StopCoroutine(dashCoroutine);
            }
            dashCoroutine = Dash(.2f, 3);
            StartCoroutine(dashCoroutine);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "trampoline")
        {
            SoundManager.PlaySound("Trampoliini");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("coin"))
        {
            SoundManager.PlaySound("Mansikka");
            Destroy(collision.gameObject);
            lm.score += 100;
        }

        if (collision.CompareTag("hp"))
        {
            SoundManager.PlaySound("Mansikka");
            Destroy(collision.gameObject);
            healthController.currentHealth = healthController.currentHealth + healthBag;
            healthController.UpdateHealth();
        }


        //if (collision.CompareTag("door"))
        //{
        //  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //Destroy(gameObject);
        //}
    }



    //Coin systeemi

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //if(other.tag == "coin")
    //{
    //lm.CoinCollected();
    //Destroy(other.gameObject);
    //}
    //}

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
