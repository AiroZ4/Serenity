using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpText : MonoBehaviour
{
    private Animator anim;
    private BoxCollider2D box;

    void Start()
    {
        anim.GetComponent<Animator>();
        box.GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Text();
            Debug.Log("Should be working");
        }
    }
    private void Text()
    {
        // Play Text Animation
        anim.SetTrigger("DashTUT");
    }
}
