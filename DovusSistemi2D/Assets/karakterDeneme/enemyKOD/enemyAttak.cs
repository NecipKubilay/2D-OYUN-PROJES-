using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class enemyAttak : MonoBehaviour
{

    //-------------------GENEL SEYLER---------------------------
    public GameObject dusmanBak�sNoktas�;
    Vector3 DusmanBak�sYonu;

    public GameObject player;


    public float range;
    public float distanceToPlayer;

    Rigidbody2D rb;
    Animator anim;

    public LayerMask playerLayers;


    public Transform attackPoint;
    public int attackDamage = 20;
    public float attackRange = 1f;



    float cooldownTime = 2f;
    private float sald�r�Zaman = 1.5f;

    private float lastAttackTime = 0.0f;

    public bool canAttack = true;
    //-------------------------------------------------------------


    

    int sayac = 0;


    
    //-----------------------EFEKTLER(vurus,defense vb)------------------------------
    public GameObject defPrefab;
    GameObject defClone;
    public GameObject vurusEfektPrefab;
    GameObject vurusEfektClone;
    //-------------------------------------------------------------




    


    

    public static enemyAttak Instance { get; private set; }


    private void Awake()
    {
        Instance = this;

    }




    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        



        if (transform.localScale.x < 0)
        {
            DusmanBak�sYonu = -transform.right;
        }
        else
        {
            DusmanBak�sYonu = transform.right;
        }


        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        //RaycastHit2D lazer = Physics2D.Raycast(dusmanBak�sNoktas�.transform.position, DusmanBak�sYonu, 20f);
        //if (lazer.collider != null)
        //{

        //    if (distanceToPlayer <= range && canAttack && enemy.Instance.sald�r�yor)
        //    {

        //        float timeSinceLastAttack = Time.time - lastAttackTime;
        //        if (timeSinceLastAttack >= sald�r�Zaman)
        //        {

        //            StartCoroutine(Sald�r�());

        //            lastAttackTime = Time.time;
        //        }



        //    }

        //}

        




        if (!enemy.Instance.oldumu)
        {
            if (distanceToPlayer <= range && canAttack && enemy.Instance.sald�r�yor)
            {

                float timeSinceLastAttack = Time.time - lastAttackTime;
                if (timeSinceLastAttack >= sald�r�Zaman)
                {
                    
                    StartCoroutine(Sald�r�());

                    lastAttackTime = Time.time;
                }

            }
        }

        if (enemy.Instance.oldumu)
        {
            return;
        }






    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


    IEnumerator Sald�r�()
    {
        enemy.Instance.canMove = false;
        canAttack = false;
        //sayac++;

        //if (sayac == 1)
        //{
        //    anim.SetTrigger("enemyAttack1");
        //}
        anim.SetTrigger("enemyAttack1");

        //if (sayac == 2)
        //{
        //    anim.SetTrigger("enemyAttack2");
        //    sayac = 0;
        //}








        if (playerAttack.instance.savundumu)
        {
            StartCoroutine(Toparlan());
            
        }
        if (!playerAttack.instance.savundumu)
        {
            yield return new WaitForSeconds(2f);

            enemy.Instance.canMove = true;
            canAttack= true;
        }
        
        
        
    }




    
    public void HASARVER()
    {


        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);

        if (!playerAttack.instance.savundumu)
        {
            foreach (var player in hitPlayer)
            {
                player.GetComponent<Hareket>().Damage(attackDamage);
                vurusEfektClone = Instantiate(vurusEfektPrefab, player.transform);
                StartCoroutine(efektSolma());

                Debug.Log("playera vurdum");
            }
        }
        if (playerAttack.instance.savundumu)
        {
            anim.SetTrigger("stun");
            defClone = Instantiate(defPrefab, this.transform);
            enemy.Instance.canMove = false;
            canAttack = false;
            StartCoroutine(efektDEFSolma());
            
        }

    }

    IEnumerator Toparlan()
    {
        yield return new WaitForSeconds(1f);

        enemy.Instance.canMove = true;
        canAttack = true;

    }



    IEnumerator efektSolma()
    {
        yield return new WaitForSeconds(0.15f);
        DestroyImmediate(vurusEfektClone, true);

    }




    IEnumerator efektDEFSolma()
    {
        yield return new WaitForSeconds(0.40f);
        DestroyImmediate(defClone, true);

    }



 
}
