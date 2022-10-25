using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

}
