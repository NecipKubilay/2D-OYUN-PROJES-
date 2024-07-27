using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HızlıDashAtma : MonoBehaviour
{
    //bool canDash = true;
    //bool isDashing;
    //float dashingPower = 3f;
    //float dashingTime = 0.2f;
    //float dashingCoolDown = 1f;

    //public Rigidbody2D rb;

    //public TrailRenderer trailRenderer;

    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
    //    {
    //        StartCoroutine(HızlıDash());
    //    }
    //}

    //IEnumerator HızlıDash()
    //{
    //    canDash = false;
    //    isDashing = true;
    //    float originalGravity = rb.gravityScale;
    //    rb.gravityScale = 0;
    //    rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        
    //    yield return new WaitForSeconds(dashingTime);
        
    //    rb.gravityScale = originalGravity;
    //    isDashing = false;
    //    yield return new WaitForSeconds(dashingCoolDown);
    //    canDash = true;
    //}
}
