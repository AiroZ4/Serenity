using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // My Variables
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private BoxCollider2D bc;
    public float jumpForce;
    public float runSpeed;
    private float dirx;
    public LayerMask ground;
    public LayerMask movingplatforms;

    // Sounds
    public AudioSource JumpSound;
    public AudioSource DashSound;

    // Dashing Variables
    public float dashSpeed;
    public float dashTime;
    public float currentDashTime;
    public bool isDashing;
    public bool allowedToDash;
    public float dashCoolDown;
    public float currentDashCoolDown = 0f;

    // WallJump Variables
    public float WallJumpX;
    public float WallJumpY;
    public float wallJumpCoolDown;
    public float currentWallJumpCoolDown = 0f;
    public bool allowedToWallJump;
    public bool isWallJumping;
    public float wallJumpTime;
    public float currentWallJumpTime = 0f;


    private enum movementState { Idle, Running, Jumping, Falling };

    // Animator
    private Animator anim;

    // Start is called once when play is pressed
    void Start()
    {
        isDashing = false;
        isWallJumping = false; 
        allowedToDash = true;
        allowedToWallJump = true;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        // Accelaration
        dirx = Input.GetAxisRaw("Horizontal");

        // Jump When Spacebar is pressed 
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            JumpSound.Play();
        }

        // Movement
        if (!isDashing && !isWallJumping)
        {
            rb.velocity = new Vector2(dirx * runSpeed, rb.velocity.y);
        }

        // Dash
        if (Input.GetButtonDown("Dash") && allowedToDash == true)
        {       
            rb.velocity = new Vector2(dirx * dashSpeed, 0);
            DashSound.Play();
            isDashing = true;
        }

        if (isDashing)
        {
            currentDashTime += Time.deltaTime;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            allowedToDash = false;
        }

        if (currentDashTime > dashTime)
        {
            isDashing = false;
        }

        if (isDashing == false)
        {
            currentDashTime = 0;
        }

        // Dash Cool Down
        if (!allowedToDash && !isDashing)
        {
            currentDashCoolDown += Time.deltaTime;
        }

        if (currentDashCoolDown > dashCoolDown)
        {
            allowedToDash = true;
        }

        if (allowedToDash == true)
        {
            currentDashCoolDown = 0;
        }

        // Wall Cooldown Stuff
        if (isWallJumping)
        {
            currentWallJumpCoolDown += Time.deltaTime;
        }

        if (currentWallJumpCoolDown > wallJumpCoolDown)
        {
            isWallJumping = false;
        }

        if (isWallJumping == false)
        {
            currentWallJumpCoolDown = 0;
        }

        if (!allowedToWallJump && !isWallJumping)
        {
            currentWallJumpTime += Time.deltaTime;
        }

        if (currentWallJumpTime > wallJumpCoolDown)
        {
            allowedToWallJump = true;
        }

        if (allowedToWallJump == true)
        {
            currentWallJumpTime = 0;
        }

        DoAnimation();
        WallJump();
    }

    void WallJump()
    {
        if (allowedToWallJump)
        {
            // Preform WallJump
            if (!IsGrounded() && IsWallRight() && Input.GetButtonDown("Jump") && allowedToWallJump)
            {
                rb.velocity = new Vector2(-WallJumpX, WallJumpY);
                sr.flipX = true;
                isWallJumping = true;
                allowedToWallJump = false;
            }

            if (IsWallRight())
            {
                if (!isWallJumping)
                {
                    rb.velocity = new Vector2(0, -0.5f);
                }
            }

            if (!IsGrounded() && IsWallLeft() && Input.GetButtonDown("Jump") && allowedToWallJump)
            {
                rb.velocity = new Vector2(WallJumpX, WallJumpY);
                sr.flipX = false;
                isWallJumping = true;
                allowedToWallJump = false;
            }

            if (IsWallLeft())
            {
                if (!isWallJumping)
                {
                    rb.velocity = new Vector2(0, -0.5f);
                }    
            }
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, 0.1f, ground);
    }

    private bool IsWallRight()
    {
        return Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.right, 0.1f, ground);
    }
    private bool IsWallLeft()
    {
        return Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.left, 0.1f, ground);
    }

    // Function for doing Animation
    private void DoAnimation()
    {
        movementState state;

        // Running Animation
        if (dirx > 0)
        {
            state = movementState.Running;
            sr.flipX = false;
        }

        else if (dirx < 0)
        {
            state = movementState.Running;
            sr.flipX = true;
        }

        else
        {
            state = movementState.Idle;
        }
        
        // Jumping / Falling Animation
        if (rb.velocity.y > 0.1f)
        {
            state = movementState.Jumping;
        }

        else if (rb.velocity.y < -0.1f)
        {
            state = movementState.Falling;
        }

        else if (isDashing == true)
        {
            state = movementState.Falling;
        }

        anim.SetInteger("state", (int)state);

    }
}
