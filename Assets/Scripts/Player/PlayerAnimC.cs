using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAnimC : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private Animator shovelAnim;

    private Timer shovelAttack;

    public bool isDisable;
    
    [SerializeField] GameObject Shovel;
    // Start is called before the first frame update
    void Start()
    {
        shovelAttack = new Timer(0.225f);
        shovelAttack.OnTime += AttackEnd;

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        shovelAnim = Shovel.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        anim.speed = PlayerGlobals.Instance.Speed;
        shovelAnim.speed = PlayerGlobals.Instance.Speed;

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


            Vector3 mouseDir = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (mouseDir.x >= 0)
            {
                sr.flipX = true;
                Shovel.GetComponent<SpriteRenderer>().flipY = true;

            }
            else
            {
                sr.flipX = false;
                Shovel.GetComponent<SpriteRenderer>().flipY = false;

            }
        }

        

    }


    public void Attack()
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

    public void Hitted()
    {
        isDisable = true;
        shovelAnim.SetInteger("State", 2);
        anim.SetInteger("State", 2);

        GetComponent<PlayerManager>().hitCD.OnTime += EnablePlayer;
    }

    public void EnablePlayer(object sender, EventArgs e)
    {
        isDisable = false;

        GetComponent<PlayerManager>().hitCD.OnTime -= EnablePlayer;


    }

}
