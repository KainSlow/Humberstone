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
    [SerializeField] float knockbackForce;
    [SerializeField] float disableTime;

    Vector3 direction;

    private void Awake()
    {
        disableTimer = new Timer(disableTime);
        disableTimer.OnTime += EnableMov;
        OnHit += ApplyKnockBack;
        OnHit += DisableMov;

    }

    private void Update()
    {
        disableTimer.Update();
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
