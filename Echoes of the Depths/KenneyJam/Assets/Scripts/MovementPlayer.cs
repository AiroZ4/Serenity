using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private BoxCollider2D bc;
    public float runSpeed;
    public float jumpForce;
    private float dirx;
    public float bounceForce;
    public float fanForce;
    public LayerMask ground;
    public LayerMask BouncePads;
    public LayerMask fan;

    public AudioSource JumpSound;
    public AudioSource DeathSound;

    private enum movementState { Idle, Running, Jumping, Falling};

   // private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
        JumpSound = GetComponent<AudioSource>();
        DeathSound = GetComponent<AudioSource>();
        // anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Accelaration
        dirx = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(dirx * runSpeed, rb.velocity.y);

        // Jump
        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
            {
                JumpSound.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }

        Animation();
        Bounce();
        Fan();
    }
    // Check if touching the ground
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, 0.1f, ground);
    }

    private void Bounce()
    {
        if (Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, 0.1f, BouncePads))
        {
            rb.velocity = new Vector2(rb.velocity.x, bounceForce);
        }
    }
    private void Fan()
    {
        if (Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, 0.1f, fan))
        {
            rb.velocity = new Vector2(rb.velocity.x, fanForce);
        }
    }

    // Animations
    private void Animation()
    {
        //movementState state;
        // Check direction and flip character sprite based on direction 
        if (dirx > 0)
        {
            //state = movementState.Running;
            sr.flipX = false;
        }
        else if (dirx < 0)
        {
            //state = movementState.Running;
            sr.flipX = true;
        }/*
        else
        {
            state = movementState.Idle;
        }
        if (rb.velocity.y > 0.1f)
        {
            state = movementState.Jumping;
        }
        else if (rb.velocity.y < -0.1f)
        {
            state = movementState.Falling;
        }
        anim.SetInteger("state", (int)state); */
    }
    
}
