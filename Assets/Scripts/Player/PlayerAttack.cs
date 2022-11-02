using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Timer lifeSpan;
    [SerializeField] float lifeTime;
    private void Awake()
    {
        lifeSpan = new Timer(lifeTime);
        lifeSpan.OnTime += Death;
    }

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
        if (collision.CompareTag("Enemy") && collision.name == "Collider" && !collision.transform.parent.GetComponent<EnemyManager>().deathTimer.isActive)
        {
            Vector3 dir = (GameObject.Find("Player").transform.position - collision.transform.position).normalized;
            dir.z = 0;

            collision.transform.parent.GetComponent<EnemyManager>().SetDir(-dir);
            collision.transform.parent.GetComponent<EnemyManager>().OnEnemyHitted(EventArgs.Empty);
        }
        else if (collision.CompareTag("Saltpeter"))
        {
            collision.transform.parent.GetComponent<SaltpeterBehavior>().OnHitted(EventArgs.Empty);
        }


    }

}
