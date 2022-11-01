using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatAnimC : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.magnitude != 0)
        {
            anim.SetInteger("State", 1);
        }
        else
        {
            anim.SetInteger("State", 0);
        }

        if(rb.velocity.x > 0)
        {
            sr.flipX = false;
        }
        else if(rb.velocity.x < 0)
        {
            sr.flipX = true;
        }


    }
}
