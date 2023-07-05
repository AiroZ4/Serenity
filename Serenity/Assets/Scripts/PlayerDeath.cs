using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private Vector3 RespawnPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        RespawnPoint = transform.position;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Trap")
        {
            Die();
            Debug.Log("You're Dead");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CheckPoint")
        {
            RespawnPoint = transform.position;
            Debug.Log("Checkpoint Touched");
        }
    }

    private void Die()
    {
        // Disable Movement
        rb.bodyType = RigidbodyType2D.Static;

        // Play Death Animation
        anim.SetTrigger("Death");
    }

    private void RestartLevel()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        transform.position = RespawnPoint;
        rb.bodyType = RigidbodyType2D.Dynamic;
        sprite.enabled = true;
    }
}
