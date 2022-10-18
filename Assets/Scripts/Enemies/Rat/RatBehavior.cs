using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatBehavior : EnemyBehavior
{
    Timer prowlTime;
    Timer idleTime;
    Vector3 target;
    [SerializeField] float prowlDistance;
    // Update is called once per frame

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

        idleTime.Update();
        prowlTime.Update();

        if (isMovingToPlayer)
        {
            MoveTowardsPoint(player.transform.position);
        }
        else
        {
            if (prowlTime.isActive)
                MoveTowardsPoint(target);
            
        }
    }

    #region Collisions

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.parent.name == player.name)  
        {
            Debug.Log("player detected");

            isMovingToPlayer = true;

            prowlTime.Stop();
            idleTime.Stop();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.transform.parent.name == player.name)
        {
            isMovingToPlayer = false;
            rb.velocity = Vector2.zero;

            idleTime.Start();
        }
    }
    #endregion


    private void SelectNewPoint()
    {
        float x = UnityEngine.Random.Range(-1f,1f);
        float y = UnityEngine.Random.Range(-1f,1f);

        target = new Vector3(x,y,0f) * prowlDistance;
    }

    #region Timer Helpers
    private void StartIdle(object sender, EventArgs e)
    {
        rb.velocity = Vector2.zero;
        idleTime.Start();
    }

    private void StartProwl(object sender, EventArgs e)
    {
        SelectNewPoint();
        prowlTime.Start();
    }
    #endregion

}
