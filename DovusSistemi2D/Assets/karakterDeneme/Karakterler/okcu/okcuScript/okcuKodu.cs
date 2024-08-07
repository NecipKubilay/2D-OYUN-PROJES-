using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class okcuKodu : MonoBehaviour
{
    //-------------------OKCUNUN SEYLERİ---------------------------
    public GameObject firePoint;
    public GameObject ArrowPrefab;
    //-------------------------------------------------------------
    


    Animator anim;
    Rigidbody2D rb;

    public bool canAttack = true;
    public float range;
    public GameObject player;


    private float saldırıZaman = 1.5f;

    private float lastAttackTime = 0.0f;

    public float distanceToPlayer;



    public static okcuKodu Instance { get; private set; }


    private void Awake()
    {
        Instance = this;

    }


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemy.Instance.oldumu)
        {
            if (canAttack && enemy.Instance.saldırıyor)
            {

                float timeSinceLastAttack = Time.time - lastAttackTime;
                if (timeSinceLastAttack >= saldırıZaman)
                {

                    StartCoroutine(Saldırı());

                    lastAttackTime = Time.time;
                }

            }


            //if (distanceToPlayer <= range && canAttack && enemy.Instance.saldırıyor)
            //{
            //    float timeSinceLastAttack = Time.time - lastAttackTime;
            //    if (timeSinceLastAttack >= saldırıZaman)
            //    {

                    

            //        lastAttackTime = Time.time;
            //    }
            //}

        }
        if (enemy.Instance.oldumu)
        {
            return;
        }
    }


    IEnumerator Saldırı()
    {
        enemy.Instance.canMove = false;
        canAttack = false;


        anim.SetTrigger("enemyATTACK1");




        yield return new WaitForSeconds(2f);
        enemy.Instance.canMove = true;
        canAttack = true;
    }

    
    
    public void OkSpawn()
    {
        
        Instantiate(ArrowPrefab, firePoint.transform.position, Quaternion.identity);
    }
}
