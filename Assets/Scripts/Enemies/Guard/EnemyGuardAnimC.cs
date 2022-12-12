using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyGuardAnimC : MonoBehaviour
{
    SpriteRenderer sr;
    Animator animator;


    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        GetComponent<EnemyGuardManager>().OnAttack += TriggerAttack;

    }


    private void Update()
    {

    }

    public void TriggerAttack(object sender, EventArgs e)
    {
        animator.SetTrigger("Attack");
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector3 dir = transform.position - collision.transform.position;

            if(dir.x >= 0)
            {
                sr.flipX = true;
            }
            else
            {
                sr.flipX = false;
            }
        }
    }
}
