using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    
    public GameObject[] waypoints;

    private int wpDest = 0;
    public float speed = 2f;

    // Update is called once per frame
    void Update()
    {
        // Find Where to move the Platform
        if (Vector2.Distance(waypoints[wpDest].transform.position, transform.position) < 0.1f)
        {
            wpDest++;
            if ( wpDest >= waypoints.Length)
            {
                wpDest = 0;
            }
        }
        // Move the Platform
        transform.position = Vector2.MoveTowards(transform.position, waypoints[wpDest].transform.position, Time.deltaTime * speed);
    }
}
