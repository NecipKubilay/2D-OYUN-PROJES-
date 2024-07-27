using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HýzlýDash : MonoBehaviour
{

    bool canDash = true;
    bool isDashing;
    float dashingPower = 5f;
    float dashingTime = 0.2f;
    float dashingCoolDown = 1f;


    Animator anim;
    public Rigidbody2D rb;
    BoxCollider2D boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(rb.velocity.x);


        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            anim.SetTrigger("Dash");
            StartCoroutine(HýzlDash());
            
        }
    }

    IEnumerator HýzlDash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        
        
        yield return new WaitForSeconds(dashingTime);
        
        isDashing = false;
        GetComponent<Hareket>().enabled = false;


        yield return new WaitForSeconds(0.8f);
        rb.gravityScale = originalGravity;
        GetComponent<Hareket>().enabled = true;
        yield return new WaitForSeconds(dashingCoolDown);
        //rb.velocity = new Vector2(0, 0);
        canDash = true;
    }
}
