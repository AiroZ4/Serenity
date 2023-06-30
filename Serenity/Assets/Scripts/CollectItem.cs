using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectItem : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    private int score = 0;
    public int value = 100;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectable"))
        {
            // Destroy Cherry
            Destroy(collision.gameObject);

            //  Increase Score
            score += value;
            Debug.Log(score);

            ScoreText.text = "Score: " + score;
        }
    }
}
