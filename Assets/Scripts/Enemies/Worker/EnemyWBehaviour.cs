using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyWBehaviour : EnemyBehavior
{
    [SerializeField] LayerMask whatCanIHit;
    [SerializeField] Transform aim;
    [SerializeField] Transform caster;
    [SerializeField] GameObject oAttack;
    private float angle;


    public bool IsMovingToPlayer;
    public bool isAngry;
    public bool isCollecting;
    private bool isBreaking;
    bool isColliding;
    public  Vector2 Dir { get; private set; }
    Transform target;
    public override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, 2f, whatCanIHit);

        if(col != null)
        {
            if (col.transform.parent.name == "Player")
            {
                if (!GetComponent<EnemyWManager>().AttackCD.isActive && isAngry)
                {
                    Attack();
                }
            }
            if(col.transform.parent.GetComponent<SaltpeterBehavior>() != null && isBreaking)
            {
                SetAngle(col.transform);
                if (!GetComponent<EnemyWManager>().AttackCD.isActive)
                {
                    Attack();
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (isMovingToPlayer)
        {
            SetAngle(player.transform);

            Dir = (player.transform.position - transform.position).normalized;

            MoveTowardsDirection(Dir);
        }
        else if (isCollecting && !isColliding || isBreaking && !isColliding)
        {
            if (target != null)
            {
                SetAngle(target);
                MoveTowardsDirection(Dir);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

    }

    private void SetAngle(Transform target)
    {

        Vector3 aimDirection = target.position - transform.position;

        Dir = aimDirection;

        angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        LimitAngle();

        aim.eulerAngles = new Vector3(0f, 0f, angle);
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.transform.CompareTag("Enemy"))
        {
            isColliding = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isColliding = false;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        OnTrigger(collision);

    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision == null || collision.transform.parent == null)
        {
            return;
        }

        if (collision.transform.parent.name == player.name)
        {
            rb.velocity = Vector2.zero;
            isMovingToPlayer = false;
            isCollecting = true;
        }
        else if (collision.transform.parent.GetComponent<SaltpeterBehavior>() != null)
        {
            rb.velocity = Vector2.zero;
            isBreaking = false;
        }
        else if (collision.transform.parent.GetComponent<SaltpeterDropBehaviour>() != null)
        {

            rb.velocity = Vector2.zero;
            isCollecting = false;

        }
    }


    private void OnTrigger(Collider2D collision)
    {
        if (collision.gameObject == null || collision == null || collision.transform.parent == null)
        {
            return;
        }

        if (collision.transform.parent.name == player.name)
        {
            if (isAngry)
            {
                isMovingToPlayer = true;
                isCollecting = false;
            }
        }
        else if (collision.transform.parent.GetComponent<SaltpeterBehavior>() != null)
        {
            if (!isMovingToPlayer)
            {
                target = collision.transform;
                Dir = (collision.transform.position - transform.position).normalized;
                isBreaking = true;
            }
        }
        else if (collision.transform.parent.GetComponent<SaltpeterDropBehaviour>() != null){
            if (!isMovingToPlayer && !isBreaking)
            {
                target = collision.transform;
                Dir = (collision.transform.position - transform.position).normalized;
                isCollecting = true;
            }
        }

    } 

    private void LimitAngle()
    {
        if (angle > 20 && angle <= 90)
        {
            angle = 20;
        }
        else if (angle > 90 && angle < 160)
        {
            angle = 160;

        }
        else if (angle > -160 && angle < -90)
        {
            angle = -160;
        }
        else if (angle < -20 && angle >= -90)
        {
            angle = -20;
        }
    }
    private void Attack()
    {
        Instantiate(oAttack, caster);
        GetComponent<EnemyWManager>().OnAttackDone(EventArgs.Empty);
    }


}
