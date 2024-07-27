using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    Animator anim;

    public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask enemyLayers;
    public int attackDamage = 20;

    public bool savundumu;


    float attackTimer = 0.0f;
    public bool atakTamamlandi = true;
    int sayac = 0;

    public GameObject vurusEfektPrefab;
    GameObject vurusEfektClone;






    public static playerAttack instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {






        //--------------------------------------------------

        Savunma();

        //--------------------------------------------------


        attackTimer += Time.deltaTime;
        
        if (Input.GetKeyDown(KeyCode.T) && attackTimer > 0.2f && Hareket.Instance.canMove)
        {
            sayac++;

            if (sayac == 1)
            {
                anim.SetTrigger("atak1");

            }
            if (sayac == 2)
            {
                anim.SetTrigger("atak2");


            }
            if (sayac == 3)
            {
                anim.SetTrigger("atak3");
                sayac = 0;
            }

            attackTimer = 0.0f;
        }
        //--------------------------------------------------

    }

    void Attack()
    {
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    //canWalk = false;
        //    anim.SetTrigger("attack1");
        //    //StartCoroutine(EnableWalkAfterAttack());
        //}
        //if (Input.GetKeyDown(KeyCode.Y))
        //{
        //    //canWalk = false;
        //    anim.SetTrigger("attack2");
        //    //StartCoroutine(EnableWalkAfterAttack());
        //}

        //anim.SetTrigger("atak1");

        Hareket.Instance.canMove = false;
        Debug.Log(Hareket.Instance.canMove);


        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        if (hitEnemies.Length > 0) // Eðer bir düþmana çarpýldýysa
        {

            //CamShke.Instance.ShakeCamera(5f, .1f);
            // ... diðer saldýrý kodlarý ...
        }

        foreach (var enemy in hitEnemies)
        {
            enemy.GetComponent<enemy>().Damage(attackDamage);
            vurusEfektClone = Instantiate(vurusEfektPrefab, enemy.transform);
            StartCoroutine(efektSolma());

            Debug.Log("we hit " + enemy.name);
        }
        StartCoroutine(EnableWalkAfterAttack());
    }

    void Savunma()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            anim.SetTrigger("def");
            savundumu = true;
            Hareket.Instance.canMove = false;

            StartCoroutine(EnableWalkAfterDEF());
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


    IEnumerator efektSolma()
    {
        yield return new WaitForSeconds(0.15f);
        DestroyImmediate(vurusEfektClone, true);

    }


    IEnumerator EnableWalkAfterAttack()
    {
        // Saldýrý animasyonunun süresini bekleyin
        yield return new WaitForSeconds(0.2f);

        // Yürüyüþü tekrar etkinleþtir

        Hareket.Instance.canMove = true;
        savundumu = false;
        

    }

    IEnumerator EnableWalkAfterDEF()
    {
        // Saldýrý animasyonunun süresini bekleyin
        yield return new WaitForSeconds(0.30f);

        // Yürüyüþü tekrar etkinleþtir

        Hareket.Instance.canMove = true;
        savundumu = false;
        

    }
}
