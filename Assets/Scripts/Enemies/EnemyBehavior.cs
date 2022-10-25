using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyBehavior : MonoBehaviour
{
    protected GameObject player;
    protected CircleCollider2D PlayerDetector;
    [SerializeField] protected float detectorRaidus;
    [SerializeField] protected float speed;

    protected bool isMovingToPlayer;

    protected Rigidbody2D rb;

    public virtual void Awake()
    {
        PlayerDetector = GetComponentInChildren<CircleCollider2D>();
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        PlayerDetector.radius = detectorRaidus;
    }

    protected virtual void MoveTowardsPoint(Vector3 targetPos)
    {
        Vector3 dirPlayer = targetPos - transform.position;
        dirPlayer.z = 0;

        rb.velocity = new Vector2(dirPlayer.x, dirPlayer.y).normalized * speed * Time.deltaTime;

    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision == null)
        {
            return;
        }

        Debug.Log(collision.gameObject.name);

        if (collision.gameObject.CompareTag("Attack"))
        {
            Vector3 dir = collision.transform.position - transform.position;
            GetComponent<EnemyManager>().OnEnemyHitted(EventArgs.Empty);
            GetComponent<EnemyManager>().SetDir(dir.normalized);
        }
    }

}
