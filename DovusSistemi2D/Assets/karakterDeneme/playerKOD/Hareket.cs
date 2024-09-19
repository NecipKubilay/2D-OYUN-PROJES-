using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hareket : MonoBehaviour
{
    private float horizontal;
    private float walkSpeed = 6f;
    private float runSpeed = 15f;
    private float jumpingPower = 20f;
    //bool canWalk = true;
    public bool canMove = true;
    public bool yerdemi;
    public bool canRun;
    bool zýplamaHakký;

    Animator anim;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;


    public GameObject vurusEfektPrefab;
    GameObject vurusEfektClone;




    public int maxHp = 100;
    public int currentHp = 0;

    //public float dashForce = 10f;
    //public float dashTime = 0.2f;





    //[SerializeField] TrailRenderer tr;



    //private bool isDashing;
    //bool canDash = true;


    public GameObject yereInmeEfekt;


    //float dashingPower = 24f;
    //float dashingtime = 0.2f;
    //float dashingCoolDown = 1f;
    //float dashTimer;


    public static Hareket Instance;


    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHp = maxHp;

    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.P))
        {

        }
        //-----------------------------------------

        //Jump();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Jump();
            StartCoroutine(Jumping());
            
        }
        if (!yerdemi)
        {
            anim.SetBool("havada", true);
        }
        //-----------------------------------------

        Flip();

        //-----------------------------------------

        //Attack();
        //Savunma();


        //-----------------------------------------

        //if (Input.GetKeyDown(KeyCode.F) && canDash)
        //{
        //    StartCoroutine(Dash());
        //}



        //-----------------------------------------

        //isGrounded();

        //------------------Movement-----------------------

        if (canMove)
        {
            //if (Input.GetKey(KeyCode.D))
            //{
            //    anim.SetBool("walk", true);

            //    Movement();
            //}
            //else if (Input.GetKey(KeyCode.A))
            //{
            //    anim.SetBool("walk", true);

            //    Movement();
            //}
            //else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
            //{
            //    anim.SetBool("walk", false);

            //    Movement();
            //}


            if (Input.GetKey(KeyCode.D) && canRun)
            {
                anim.SetBool("run", true);

                RunMovement();
            }
            else if (Input.GetKey(KeyCode.A)  && canRun)
            {
                anim.SetBool("run", true);

                RunMovement();
            }
            else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
            {
                anim.SetBool("run", false);

                RunMovement();
            }


        }



        //-----------------------------------------



        //-----------------------------------------


    }

    //void FixedUpdate()
    //{
    //    Movement();

    //}









    void Flip()
    {
        if (canMove)
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.localScale = new Vector3(-10, 10, 1);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.localScale = new Vector3(10, 10, 1);
            }
        }

    }


    private float timer;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Ground")
        {
            // Karakter yere deðdi
            Debug.Log("karakter yere deydi");
            yerdemi = true;


        }
    }



    bool isGrounded()
    {
        Debug.Log("yere dokundu");
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

    }
    void Jump()
    {

        anim.SetTrigger("jump");
        
        yerdemi = false;
        

        if (!yerdemi)
        {
            Debug.Log("kontrol 1");
            anim.SetBool("run", false);
        }




        do
        {
            canRun = true;
            Debug.Log("geldi adam");
            anim.SetTrigger("yereDokundu");
            
        } while (yerdemi);


        //Debug.Log(isGrounded());

        //if (isGrounded() == true)
        //{
        //    anim.SetTrigger("yereDokundu");
        //    Instantiate(yereInmeEfekt, groundCheck.transform.position, Quaternion.identity);

        //}


    }


    IEnumerator Jumping()
    {

        rb.velocity = new Vector2(rb.velocity.x , jumpingPower);
        anim.SetTrigger("jump");
        yerdemi = false;
        

        if (!yerdemi)
        {
            anim.SetBool("run", false);
        }

        yield return new WaitForSeconds(0.5f);

        do
        {
           
            Debug.Log("geldi adam");
            anim.SetTrigger("yereDokundu");

        } while (yerdemi);
    }

    //public void JumpForce()
    //{
    //    rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
    //}
    IEnumerator zýplamaZamanAralýgý()
    {
        yield return new WaitForSeconds(0.65f);
        zýplamaHakký = true;
    }

    //void Movement()
    //{

    //    if (canMove)
    //    {
    //        transform.Translate(horizontal * Time.deltaTime * walkSpeed, 0, 0);
    //    }
    //}
    void RunMovement()
    {

        if (canMove && canRun)
        {
            transform.Translate(horizontal * Time.deltaTime * runSpeed, 0, 0);
        }
    }


    //void Attack()
    //{
    //    if (Input.GetKeyDown(KeyCode.T))
    //    {
    //        canMove = false;
    //        anim.SetTrigger("attack1");
    //        StartCoroutine(EnableWalkAfterAttack());
    //    }
    //    if (Input.GetKeyDown(KeyCode.Y))
    //    {
    //        canMove = false;
    //        anim.SetTrigger("attack2");
    //        StartCoroutine(EnableWalkAfterAttack());
    //    }
    //}

    //void Savunma()
    //{
    //    if (Input.GetKeyDown(KeyCode.Y))
    //    {
    //        anim.SetBool("def", true);
    //        canMove = false;
    //    }
    //    if (Input.GetKeyUp(KeyCode.Y))
    //    {
    //        anim.SetBool("def", false);
    //        StartCoroutine(EnableWalkAfterAttack());
    //    }

    //}

    public void Damage(int damage)
    {
        currentHp = currentHp - damage;
        canMove = false;

        if (enemy.Instance != null)
        {

            if (enemy.Instance.transform.localScale.x < 0)
            {
                Vector2 force = new Vector2(-2000, 0);
                rb.AddForce(force);
            }
            if (enemy.Instance.transform.localScale.x > 0)
            {

                Vector2 force = new Vector2(2000, 0);
                rb.AddForce(force);

            }
        }

        if (BossKod.Instance != null)
        {
            if (BossKod.Instance.transform.localScale.x < 0)
            {
                Vector2 force = new Vector2(-2000, 0);
                rb.AddForce(force);
            }
            if (BossKod.Instance.transform.localScale.x > 0)
            {

                Vector2 force = new Vector2(2000, 0);
                rb.AddForce(force);

            }
        }


        vurusEfektClone = Instantiate(vurusEfektPrefab, this.transform);
        StartCoroutine(efektSolma());

        if (currentHp > 0)
        {
            anim.SetTrigger("HURT");

        }

        if (currentHp <= 0)
        {
            // Nesneyi yok etmek yerine, canýný 0'a ayarlayýn.
            currentHp = 0;

            // Karakterin öldüðünü gösteren bir fonksiyon çaðýrýn.
            Debug.Log("öldüm");
        }

        StartCoroutine(EnableMoveAfterDamage());
    }


    IEnumerator efektSolma()
    {
        yield return new WaitForSeconds(0.15f);
        DestroyImmediate(vurusEfektClone, true);

    }




    //IEnumerator EnableWalkAfterAttack()
    //{
    //    // Saldýrý animasyonunun süresini bekleyin
    //    yield return new WaitForSeconds(0.30f);

    //    // Yürüyüþü tekrar etkinleþtir
    //    canMove = true;
    //}

    IEnumerator EnableMoveAfterDamage()
    {
        yield return new WaitForSeconds(0.3f);
        canMove = true;
    }
}
