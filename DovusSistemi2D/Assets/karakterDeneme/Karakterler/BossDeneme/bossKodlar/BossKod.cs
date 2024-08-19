using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossKod : MonoBehaviour
{

    //--------------------------------------
    [SerializeField] float speed = 2.5f;
    [SerializeField] float rageSpeed = 5f;
    Rigidbody2D rb;
    public Transform player;
    Animator anim;
    public bool isFlipped = false;
    public bool canMove = true;
    public bool canAttack = true;
    public bool oldumu;
    bool inRage;
    //--------------------------------------
    public GameObject vurusEfektPrefab;
    GameObject vurusEfektClone;

    public GameObject ragePatlamaEfektPrefab;
    GameObject ragePatlamaEfektClone;

    public GameObject rageFlameEfektPrefab;
    GameObject rageFlameEfektClone;
    BoxCollider2D collider;
    int sayac;

    //--------------------------------------
    public int maxHp = 100;
    [SerializeField] int currentHp = 0;
    //--------------------------------------
    public Transform attackPoint;
    public int attackDamage = 20;
    [SerializeField] float attackRange;
    [SerializeField] float range;
    public LayerMask playerLayers;
    //--------------------------------------


    public static BossKod Instance;


    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //------------------------------------------------

        LookAtPlayer();

        //------------------------------------------------

        moveToPlayer();

        //------------------------------------------------

        bossAttak();

        //------------------------------------------------
    }


    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;



        if (transform.position.x < player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x > player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }


    }


    void moveToPlayer()
    {
        if (canMove)
        {
            if (!inRage)
            {
                Vector2 target = new Vector2(player.position.x, rb.position.y);
                Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
                rb.MovePosition(newPos);
            }

            if (inRage)
            {
                Vector2 target = new Vector2(player.position.x, rb.position.y);
                Vector2 newPos = Vector2.MoveTowards(rb.position, target, rageSpeed * Time.fixedDeltaTime);
                rb.MovePosition(newPos);
            }
        }

    }

    public float distans;
    void bossAttak()
    {
        
        distans = Vector2.Distance(player.position, transform.position);
        Debug.Log(distans);
        if (Vector2.Distance(player.position, transform.position) <= range && canAttack)
        {
            StartCoroutine(Saldýrý());
            Debug.Log("bossatak");
        }


    }


    public void Damage(int damage)
    {
        //dokundu = true;

        currentHp = currentHp - damage;
        Debug.Log(currentHp);



        canAttack = false;
        canMove = false;

        vurusEfektClone = Instantiate(vurusEfektPrefab, this.transform);
        StartCoroutine(efektSolma());
        StartCoroutine(kotektenSonraToparlanma());


        //if (currentHp > 0)
        //{
        //    anim.SetTrigger("HURT");

        //}
        if (currentHp <= 50)
        {
            anim.SetTrigger("RAGE");
            canMove = false;
            canAttack = false;
            StartCoroutine(RageSpawnBekle());
            ragePatlamaEfekt();
            inRage = true;
            return;
        }

        if (currentHp <= 0)
        {
            // Nesneyi yok etmek yerine, canýný 0'a ayarlayýn.
            currentHp = 0;
            oldumu = true;
            // Karakterin öldüðünü gösteren bir fonksiyon çaðýrýn.
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



    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


    IEnumerator Saldýrý()
    {

        Debug.Log("Saldýrý");
        


        if (canAttack)
        {
            if (!inRage)
            {
                sayac++;

                if (sayac == 1)
                {
                    anim.SetTrigger("Attak");
                }


                if (sayac == 2)
                {
                    anim.SetTrigger("Attak2");
                    sayac = 0;
                }
            }
            if (inRage)
            {
                anim.SetTrigger("RAGEATTACK");
            }
        }

        canAttack = false;
        canMove = false;





        if (playerAttack.instance.savundumu)
        {
            StartCoroutine(Toparlan());

        }
        if (!playerAttack.instance.savundumu)
        {
            yield return new WaitForSeconds(2f);
            Debug.Log("burdan gecti");
            canMove = true;
            canAttack = true;
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
                //vurusEfektClone = Instantiate(vurusEfektPrefab, player.transform);
                //StartCoroutine(efektSolma());

                Debug.Log("playera vurdum");
            }
        }
        //if (playerAttack.instance.savundumu)
        //{
        //    anim.SetTrigger("stun");
        //    defClone = Instantiate(defPrefab, this.transform);
        //    enemy.Instance.canMove = false;
        //    canAttack = false;
        //    StartCoroutine(efektDEFSolma());

        //}

    }


    public void ragePatlamaEfekt()
    {
        ragePatlamaEfektClone = Instantiate(ragePatlamaEfektPrefab, attackPoint.transform);
        StartCoroutine(ragePatlamaEfektSolma());
    }
    IEnumerator ragePatlamaEfektSolma()
    {
        yield return new WaitForSeconds(0.21f);
        DestroyImmediate(ragePatlamaEfektClone, true);

    }


    IEnumerator Toparlan()
    {
        yield return new WaitForSeconds(2f);

        canMove = true;
        canAttack = true;

    }

    IEnumerator efektSolma()
    {
        yield return new WaitForSeconds(0.15f);
        DestroyImmediate(vurusEfektClone, true);

    }

    IEnumerator kotektenSonraToparlanma()
    {
        yield return new WaitForSeconds(2f);

        canAttack = true;
        canMove = true;
    }

    IEnumerator RageSpawnBekle()
    {
        yield return new WaitForSeconds(4f);
        canAttack = true;
        canMove = true;
    }
}
