using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Start is called before the first frame update

    public EventHandler OnHit;
    Rigidbody2D rb;
    Timer disableTimer;
    public Timer deathTimer;
    [SerializeField] float knockbackForce;
    [SerializeField] float disableTime;
    [SerializeField] float deathTime;
    [SerializeField] int lifes;
    SpriteRenderer sr;

    Vector3 direction;

    private void Awake()
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

    private void Damage(object sender, EventArgs e)
    {
        lifes--;
    }

    private void Death(object sender, EventArgs e)
    {
        Destroy(gameObject);
    }

    private void isDeath(object sender, EventArgs e)
    {
        if(lifes <= 0) {
            deathTimer.Start();
        }
    }

    private void Update()
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

    private void DisableMov(object sender, EventArgs e)
    {
        GetComponent<EnemyBehavior>().enabled = false;
        disableTimer.Start();
    }

    private void EnableMov(object sender, EventArgs e)
    {
        GetComponent<EnemyBehavior>().enabled = true;
    }

    private void ApplyKnockBack(object sender, EventArgs e)
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(direction * knockbackForce,ForceMode2D.Impulse);
    }


}
