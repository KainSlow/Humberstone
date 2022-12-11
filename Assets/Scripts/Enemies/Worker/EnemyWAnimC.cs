using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyWAnimC : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private Animator shovelAnim;

    private Timer shovelAttack;

    public bool isDisable;

    [SerializeField] GameObject Shovel;


    void Start()
    {
        shovelAttack = new Timer(0.3f);
        shovelAttack.OnTime += AttackEnd;

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        shovelAnim = Shovel.GetComponent<Animator>();
        GetComponent<EnemyWManager>().OnAttack += Attack;
        GetComponent<EnemyManager>().OnHit += Hitted;
    }

    // Update is called once per frame
    void Update()
    {
        shovelAttack.Update();

        if (!isDisable)
        {
            if (rb.velocity.magnitude != 0)
            {
                anim.SetInteger("State", 1);
                shovelAnim.SetInteger("State", 1);
            }
            else
            {
                anim.SetInteger("State", 0);
                shovelAnim.SetInteger("State", 0);

            }

            Vector3 dir = GetComponent<EnemyWBehaviour>().Dir;

            if (dir.x >= 0)
            {
                sr.flipX = false;
                Shovel.GetComponent<SpriteRenderer>().flipY = false;

            }
            else
            {
                sr.flipX = true;
                Shovel.GetComponent<SpriteRenderer>().flipY = true;
            }
        }

        if (GetComponent<EnemyWBehaviour>().isAngry)
        {
            sr.color = Color.red;
            shovelAnim.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            sr.color = Color.white;
            shovelAnim.gameObject.GetComponent<SpriteRenderer>().color = Color.white;

        }
    }


    public void Attack(object sender, EventArgs e)
    {
        if (!shovelAnim.GetBool("isAttacking"))
        {
            shovelAnim.SetBool("isAttacking", true);
            shovelAttack.Start();
        }
    }

    void AttackEnd(object sender, EventArgs e)
    {
        shovelAnim.SetBool("isAttacking", false);
    }

    public void Hitted(object sender, EventArgs e)
    {
        isDisable = true;
        shovelAnim.SetInteger("State", 2);
        anim.SetInteger("State", 2);
        GetComponent<EnemyManager>().disableTimer.OnTime += EnableEnemy;
    }

    public void EnableEnemy(object sender, EventArgs e)
    {
        isDisable = false;
        GetComponent<EnemyManager>().disableTimer.OnTime -= EnableEnemy;

    }
}
