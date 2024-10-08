using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;

    //public Transform[] patrolPoints;

    Animator anim;

    public bool saldırıyor;
    public bool dokundu;
    public bool oldumu;
    public bool canMove = true;


    public GameObject player;

    public Transform target;

    [SerializeField]float speed = 2f;
    [SerializeField] float runspeed = 4f;

    //-------------------------------------


    BoxCollider2D collider;

    public float distanceToPlayer;
    //float kovalamaRange = 5f;
    //Color gizmoColor = Color.green;
    //float gorusAlaniUzunlugu = 5f;
    //bool oyuncuGoruldu;
    //float distance = 5f;
    Vector3 DusmanBakısYonu;
    public GameObject dusmanBakısNoktası;

    bool gordumu;
    //-------------------------------------

    public static enemy Instance { get; private set; }


    private void Awake()
    {
        Instance = this;

    }


    //-------------------------------------

    public int maxHp = 100;
    [SerializeField] int currentHp = 0;



    public GameObject vurusEfektPrefab;
    GameObject vurusEfektClone;


    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        pointA = GameObject.FindGameObjectWithTag("pointA");
        pointB = GameObject.FindGameObjectWithTag("pointB");
        player = GameObject.FindGameObjectWithTag("Player");

        //Vector3 pointA1 = new Vector3(-1, transform.position.y, transform.position.z);
        //Vector3 pointA2 = new Vector3(3, transform.position.y, transform.position.z);




        target = pointA.transform;


        currentHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {

        //distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (!oldumu)
        {
            
            if (!dokundu)
            {
                if (transform.localScale.x < 0)
                {
                    DusmanBakısYonu = -transform.right;
                }
                else
                {
                    DusmanBakısYonu = transform.right;
                }

                int katmanMask = LayerMask.GetMask("player");

                RaycastHit2D lazer = Physics2D.Raycast(dusmanBakısNoktası.transform.position, DusmanBakısYonu, 20f, katmanMask);
                if (lazer.collider != null)
                {
                    if (lazer.collider.gameObject.CompareTag("Player"))
                    {

                        Debug.DrawLine(dusmanBakısNoktası.transform.position, dusmanBakısNoktası.transform.position + DusmanBakısYonu * 20f, Color.red);
                        
                        SaldırıModu();

                        string nesneIsmi = lazer.collider.gameObject.name;

                        gordumu = true;
                    }
                }
                if (lazer.collider == null)
                {
                    //if (gordumu)
                    //{
                    //    SaldırıModu();
                    //}
                    //if (!gordumu)
                    //{
                    //    anim.SetBool("isRun", false);

                    //    Devriye();
                    //    Debug.DrawLine(dusmanBakısNoktası.transform.position, dusmanBakısNoktası.transform.position + DusmanBakısYonu * 5f, Color.green);
                    //}

                    //anim.SetBool("isRun", false);

                    Devriye();
                    Debug.DrawLine(dusmanBakısNoktası.transform.position, dusmanBakısNoktası.transform.position + DusmanBakısYonu * 20f, Color.green);
                }
            }
            if (dokundu)
            {
                SaldırıModu();
            }
        }

        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            dokundu = true;
        }
    }



    void SaldırıModu()
    {
        saldırıyor = true;


        if (canMove)
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isRun", true);

            if (transform.position.x < player.transform.position.x)
            {
                transform.localScale = new Vector3(10, 10, 1);
            }
            if (transform.position.x > player.transform.position.x)
            {
                transform.localScale = new Vector3(-10, 10, 1);
            }


            Vector3 hedefPozisyon = player.transform.position;
            hedefPozisyon.y = transform.position.y;

            //Vector3 directionToPlayer = player.transform.position - transform.position;
            if (distanceToPlayer >= enemyAttak.Instance.range)
            {
                transform.position = Vector3.MoveTowards(transform.position, hedefPozisyon, runspeed * Time.deltaTime);
            }
            
            

            distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= enemyAttak.Instance.range)
            {
                anim.SetBool("isRun", false);
            }

            //float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            //if (distanceToPlayer <= enemyAttack.Instance.range)
            //{
            //    anim.SetBool("iswalking", false);
            //}

        }
        if (!canMove)
        {
            anim.SetBool("isRun", false);
        }


    }




    //void gorusAlani()
    //{


    //    if (transform.localScale.x < 0)
    //    {
    //        DusmanBakısYonu = -transform.right;
    //    }
    //    else
    //    {
    //        DusmanBakısYonu = transform.right;
    //    }

    //    Vector3 endPos = transform.position + Vector3.right, gorusAlaniUzunlugu;
    //    RaycastHit2D lazer = Physics2D.Raycast(transform.position, transform.right, 5f);


    //    if (lazer.collider != null)
    //    {
    //        string nesneIsmi = lazer.collider.gameObject.name;
    //        if (lazer.collider.CompareTag("player"))
    //        {
    //            Debug.Log("Lazer, " + nesneIsmi + " nesnesine çarptı!");

    //            SaldırıModu();
    //        }

    //        Devriye();
    //    }

    //}






    void Devriye()
    {
        saldırıyor = false;

        if (canMove)
        {
            anim.SetBool("isRun", false);
            anim.SetBool("isWalking", true);


            if (target.transform.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-10, 10, 1);
            }
            if (target.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(10, 10, 1);
            }



            if (transform.position == pointA.transform.position)
            {

                target = pointB.transform;
            }
            if (transform.position == pointB.transform.position)
            {

                target = pointA.transform;
            }




            target.position = new Vector3(target.position.x, transform.position.y, transform.position.z);

            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else
        {
            //anim.SetBool("enemyWait", true);
            anim.SetBool("isWalking", false);
        }

    }




    
    public void Damage(int damage)
    {
        dokundu = true;

        currentHp = currentHp - damage;
        Debug.Log(currentHp);

        //-------------------------------------------

        if (Hareket.Instance != null)
        {

            if (Hareket.Instance.transform.localScale.x < 0)
            {
                Vector2 force = new Vector2(-200, 200);
                rb.AddForce(force);
            }
            if (Hareket.Instance.transform.localScale.x > 0)
            {

                Vector2 force = new Vector2(200, 200);
                rb.AddForce(force);

            }
        }

        //-------------------------------------------

        enemyAttak.Instance.canAttack = false;
        canMove = false;

        vurusEfektClone = Instantiate(vurusEfektPrefab, this.transform);
        StartCoroutine(efektSolma());
        StartCoroutine(kotektenSonraToparlanma());


        if (currentHp > 0)
        {
            anim.SetTrigger("enemyHURT");

        }

        if (currentHp <= 0)
        {
            // Nesneyi yok etmek yerine, canını 0'a ayarlayın.
            currentHp = 0;
            oldumu = true;
            // Karakterin öldüğünü gösteren bir fonksiyon çağırın.
            OnDeath();
        }
    }


    void OnDeath()
    {
        collider.isTrigger = true;
        rb.simulated = false;
        anim.SetTrigger("DEATH");
        Destroy(this.gameObject, 2f);
        //Destroy(this.gameObject);
    }



    IEnumerator kotektenSonraToparlanma()
    {
        yield return new WaitForSeconds(0.5f);

        enemyAttak.Instance.canAttack = true;
        canMove = true;
    }


    IEnumerator efektSolma()
    {
        yield return new WaitForSeconds(0.15f);
        DestroyImmediate(vurusEfektClone, true);

    }

}










//if (distanceToPlayer <= kovalamaRange)
//{

//    anim.SetBool("isWalking", false);
//    SaldırıModu();


//}
//if (distanceToPlayer >= kovalamaRange)
//{

//    anim.SetBool("isRun", false);
//    Devriye();
//}