using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemySlash : MonoBehaviour
{
    Timer lifeSpan;
    [SerializeField] float lifeTime;
    private void Awake()
    {
        lifeSpan = new Timer(lifeTime);
        lifeSpan.OnTime += Death;
    }


    bool SalpeterHitted;
    bool PlayerHitted;

    private void Start()
    {
        lifeSpan.Start();
    }
    void Update()
    {
        GetComponent<SpriteRenderer>().flipY = GetComponentInParent<SpriteRenderer>().flipY;

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
                pm.direction = (collision.transform.position - transform.parent.position).normalized;
                pm.OnPlayerHitted(EventArgs.Empty);
                PlayerHitted = true;
            }
        }
        if (collision.CompareTag("Saltpeter"))
        {
            collision.transform.parent.GetComponent<SaltpeterBehavior>().OnHitted(EventArgs.Empty);
            SalpeterHitted = true;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !PlayerHitted)
        {
            PlayerManager pm = collision.transform.parent.GetComponent<PlayerManager>();
            if (pm != null)
            {

                pm.direction = (collision.transform.position - transform.parent.position).normalized;
                pm.OnPlayerHitted(EventArgs.Empty);
                PlayerHitted = true;

            }
        }
        if (collision.CompareTag("Saltpeter") && !SalpeterHitted)
        {
            collision.transform.parent.GetComponent<SaltpeterBehavior>().OnHitted(EventArgs.Empty);
            SalpeterHitted = true;

        }
    }
}
