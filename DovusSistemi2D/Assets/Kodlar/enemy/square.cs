using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class square : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        RaycastHit2D lazer = Physics2D.Raycast(transform.position, transform.right ,5f);

        if (lazer.collider != null)
        {
            string nesneIsmi = lazer.collider.gameObject.name;
            if (lazer.collider.CompareTag("Player"))
            {
                Debug.Log("Lazer, " + nesneIsmi + " nesnesine �arpt�!");
                Debug.Log("31 seksi seksi siki� soku�");
            }
            
            
        }


        Debug.DrawLine(transform.position, transform.position + transform.right * 5f, Color.green);
    }
}
