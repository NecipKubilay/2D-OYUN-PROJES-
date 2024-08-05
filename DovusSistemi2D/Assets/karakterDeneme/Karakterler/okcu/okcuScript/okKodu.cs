using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class okKodu : MonoBehaviour
{

    public GameObject player;
    public Rigidbody2D rb;
    public float force;
    public int attackDamage;
    public GameObject vurusEfektPrefab;
    GameObject vurusEfektClone;



    public static okKodu Instance { get; private set; }


    private void Awake()
    {
        Instance = this;

    }


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

    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Hareket.Instance.GetComponent<Hareket>().Damage(attackDamage);
            vurusEfektClone = Instantiate(vurusEfektPrefab, player.transform);
            StartCoroutine(efektSolma());
            
            
        }
    }

    IEnumerator efektSolma()
    {
        yield return new WaitForSeconds(0.15f);
        DestroyImmediate(vurusEfektClone, true);
        Destroy(this.gameObject);
    }


}
