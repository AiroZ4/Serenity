using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Vector3 RespawnPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        RespawnPoint = transform.position;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
            Debug.Log("You're Dead");
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
        anim.SetTrigger("Respawn");
    }
}
