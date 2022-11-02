using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Start is called before the first frame update

    public EventHandler OnHit;
    protected Rigidbody2D rb;
    public Timer disableTimer;
    public Timer deathTimer;
    [SerializeField] float knockbackForce;
    [SerializeField] float disableTime;
    [SerializeField] float deathTime;
    [SerializeField] int lifes;
    SpriteRenderer sr;

    Vector3 direction;

    protected virtual void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        disableTimer = new Timer(disableTime);
        disableTimer.OnTime += EnableMov;
        disableTimer.OnTime += isDeath;

        OnHit += ApplyKnockBack;
        OnHit += DisableMov;
        OnHit += Damage;

        deathTimer = new Timer(deathTime);
        deathTimer.OnTime += Death;
    }

    protected void Damage(object sender, EventArgs e)
    {
        lifes--;
    }

    protected void Death(object sender, EventArgs e)
    {
        Destroy(gameObject);
    }

    protected void isDeath(object sender, EventArgs e)
    {
        if(lifes <= 0) {
            deathTimer.Start();
        }
    }

    protected virtual void Update()
    {
        disableTimer.Update();
        if (disableTimer.isActive)
        {
            sr.color = new Color(sr.color.r,sr.color.g,sr.color.b, Mathf.Abs(Mathf.Cos(disableTimer.CurrentTime * 50)));
        }
        else
        {
            if (!deathTimer.isActive)
            {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);

            }
        }

        deathTimer.Update();
        if (deathTimer.isActive)
        {
            GetComponent<EnemyBehavior>().DeathBehaviour();
        }
    }

    public virtual void OnEnemyHitted(EventArgs e)
    {
        EventHandler handler = OnHit;
        handler?.Invoke(this, e);
    }

    public void SetDir(Vector3 dir) => direction = dir;

    protected void DisableMov(object sender, EventArgs e)
    {
        GetComponent<EnemyBehavior>().enabled = false;
        disableTimer.Start();
    }

    protected void EnableMov(object sender, EventArgs e)
    {
        rb.velocity = Vector2.zero;
        GetComponent<EnemyBehavior>().enabled = true;
    }

    protected void ApplyKnockBack(object sender, EventArgs e)
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(direction * knockbackForce,ForceMode2D.Impulse);
    }


}
