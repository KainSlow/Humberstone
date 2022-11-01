using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatBehavior : EnemyBehavior
{
    Timer prowlTime;
    Timer idleTime;
    Vector3 target;
    [SerializeField] LayerMask whatIsWall;
    // Update is called once per frame
    private bool dead;
    public override void Awake()
    {
        base.Awake();
        prowlTime = new Timer(1f);
        idleTime = new Timer(2f);

        prowlTime.OnTime += StartIdle;
        idleTime.OnTime += StartProwl;
        idleTime.Start();

    }


    void FixedUpdate()
    {
        if (!dead)
        {
            idleTime.Update();
            prowlTime.Update();

            if (isMovingToPlayer)
            {
                Vector3 dirPlayer = (transform.position - player.transform.position).normalized;
                dirPlayer.z = 0;
                MoveTowardsDirection(-dirPlayer);
            }
            else
            {
                if (prowlTime.isActive)
                {
                    MoveTowardsDirection(target);
                }
            }
        }
        
    }

    #region Collisions

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == null || collision == null || collision.transform.parent == null)
        {
            return;
        }

        if(collision.transform.parent.name == player.name)
        {
            isMovingToPlayer = true;

            prowlTime.Stop();
            idleTime.Stop();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if(collision == null || collision.transform.parent == null)
        {
            return;
        }

        if(collision.transform.parent.name == player.name)
        {
            isMovingToPlayer = false;
            rb.velocity = Vector2.zero;
            idleTime.Start();
        }

    }
    #endregion

    private void SelectNewDirection()
    {
        target = UnityEngine.Random.insideUnitCircle;
        target.z = 0f;
    }

    #region Timer Helpers
    private void StartIdle(object sender, EventArgs e)
    {
        rb.velocity = Vector2.zero;
        idleTime.Start();
    }

    private void StartProwl(object sender, EventArgs e)
    {
        SelectNewDirection();
        prowlTime.Start();
    }
    #endregion

    public override void DeathBehaviour()
    {
        Vector3 dir = (transform.position - player.transform.position).normalized;
        rb.velocity = dir * speed * 3.5f * Time.deltaTime;

        var sr = GetComponentInChildren<SpriteRenderer>();
        sr.color = new Color(sr.color.r,sr.color.g,sr.color.b,sr.color.a - 0.5f * Time.deltaTime);

        dead = true; ;
    }

}
