using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    protected GameObject player;
    [SerializeField] protected float speed;

    protected bool isMovingToPlayer;

    protected Rigidbody2D rb;

    public virtual void Awake()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void MoveTowardsPoint(Vector3 targetPos)
    {
        Vector3 dirPlayer = targetPos - transform.position;
        dirPlayer.z = 0;

        rb.velocity = new Vector2(dirPlayer.x, dirPlayer.y).normalized * speed * Time.deltaTime;

    }

}
