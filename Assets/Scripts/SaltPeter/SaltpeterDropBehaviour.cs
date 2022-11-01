using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class SaltpeterDropBehaviour : MonoBehaviour
{
    [SerializeField] float jumpForce;
    [SerializeField] float endTime;
    Rigidbody2D rb;
    Timer EndTimer;
    Vector2 jumpDir;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        EndTimer = new Timer(endTime);

        EndTimer.OnTime += NoGravity;
        do
        {
            jumpDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-0.5f, 1f));

        } while (jumpDir.magnitude == 0f);

        rb.AddForce(jumpDir * jumpForce ,ForceMode2D.Impulse);
        EndTimer.Start();
    }

    // Update is called once per frame
    void Update()
    {
        EndTimer.Update();   
    }

    private void NoGravity(object sender, EventArgs e)
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !EndTimer.isActive)
        {
            if(PlayerGlobals.Instance.Saltpeter < PlayerGlobals.Instance.maxSaltpeter)
            {
                PlayerGlobals.Instance.AddSaltpeter();
                Destroy(gameObject);
            }
        }
        if (collision.CompareTag("Wall"))
        {
            EndTimer.Stop(true);
        }

    }
}
