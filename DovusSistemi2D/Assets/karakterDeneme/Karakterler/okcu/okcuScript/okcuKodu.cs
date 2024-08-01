using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class okcuKodu : MonoBehaviour
{
    //-------------------OKCUNUN SEYLERÝ---------------------------
    public GameObject firePoint;
    public GameObject ArrowPrefab;
    //-------------------------------------------------------------
    


    Animator anim;
    Rigidbody2D rb;

    public bool canAttack = true;
    public float range;
    public GameObject player;


    private float saldýrýZaman = 1.5f;

    private float lastAttackTime = 0.0f;

    public float distanceToPlayer;


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
            if (canAttack && enemy.Instance.saldýrýyor)
            {

                float timeSinceLastAttack = Time.time - lastAttackTime;
                if (timeSinceLastAttack >= saldýrýZaman)
                {

                    StartCoroutine(Saldýrý());

                    lastAttackTime = Time.time;
                }

            }


            //if (distanceToPlayer <= range && canAttack && enemy.Instance.saldýrýyor)
            //{
            //    float timeSinceLastAttack = Time.time - lastAttackTime;
            //    if (timeSinceLastAttack >= saldýrýZaman)
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


    IEnumerator Saldýrý()
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
