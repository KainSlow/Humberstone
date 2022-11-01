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

    protected virtual void MoveTowardsDirection(Vector3 dirTarget)
    {
        rb.velocity = new Vector2(dirTarget.x, dirTarget.y).normalized * speed * Time.deltaTime;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    public virtual void DeathBehaviour()
    {

    }
}
