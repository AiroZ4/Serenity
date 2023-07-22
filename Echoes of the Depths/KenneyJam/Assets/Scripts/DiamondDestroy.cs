using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondDestroy : MonoBehaviour
{
    public GameObject Invisible1;
    

    // Start is called before the first frame update
    void Start()
    {
    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Invisible1.SetActive(true);
        Debug.Log("Next area is avalible");
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
