using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class okKodu : MonoBehaviour
{

    public GameObject player;
    public Rigidbody2D rb;
    public float force;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        if (direction.x < 0)
        {
            rb.velocity = -transform.right * force;
            transform.localScale *= -1;
        }
        if (direction.x > 0)
        {
            rb.velocity = transform.right * force;
            transform.localScale *= 1;
        }


    }

    
}
