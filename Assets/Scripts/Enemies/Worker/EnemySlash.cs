using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemySlash : MonoBehaviour
{
    Timer lifeSpan;
    [SerializeField] float lifeTime;
    SpriteRenderer sr;

    private void Awake()
    {
        lifeSpan = new Timer(lifeTime);
        lifeSpan.OnTime += Death;
    }

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        Vector3 dir = (gameObject.transform.parent.position - transform.position).normalized;

        if(dir.x >= 0)
        {
            sr.flipY = true;
        }
        else
        {
            sr.flipY = false;
        }


        lifeSpan.Start();
    }
    void Update()
    {
        lifeSpan.Update();
    }
    private void Death(object sender, EventArgs e)
    {
        Destroy(this.gameObject);
    }
        
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerManager pm = collision.transform.parent.GetComponent<PlayerManager>();
            if (pm != null)
            {
                if (!pm.hitCD.isActive)
                {
                    pm.direction = (collision.transform.position - transform.parent.position).normalized;
                    pm.OnPlayerHitted(EventArgs.Empty);
                }

            }
        }
        if (collision.CompareTag("Saltpeter"))
        {
            collision.transform.parent.GetComponent<SaltpeterBehavior>().OnHitted(EventArgs.Empty);
        }

    }

}
