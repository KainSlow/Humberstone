using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public event EventHandler OnClick;
    public event EventHandler OnHit;

    float attackCD;
    [SerializeField] float disableTime;
    [SerializeField] float knockbackDuration;
    [SerializeField] float knockbackForce;
    [SerializeField] GameObject saltpeterDrop;

    Rigidbody2D rb;

    public Timer disableTimer;
    public Timer AttackCadence;
    public Timer hitCD;

    private Vector3 direction;
    private void Awake()
    {
        //OnClick += ApplyFowardForce;
        OnHit += ApplyKnockBack;
        OnHit += DropSaltpeter;

        rb = GetComponent<Rigidbody2D>();
        disableTimer = new Timer(disableTime);
        disableTimer.OnTime += EnableMov;

        attackCD = PlayerGlobals.Instance.Cadence;

        AttackCadence = new Timer(attackCD);
        AttackCadence.OnTime += EnableAim;

        hitCD = new Timer(knockbackDuration);
        hitCD.OnTime += EnableMov;
        hitCD.OnTime += EnableAim;

        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);

    }

    private void Start()
    {
        GameObject cam = GameObject.Find("CameraHolder");
        cam.GetComponent<CameraMov>().player = transform;
    }

    public virtual void OnMouseClicked(EventArgs e)
    {
        EventHandler handler = OnClick;
        handler?.Invoke(this, e);
    }

    public virtual void OnPlayerHitted(EventArgs e)
    {
        EventHandler handler = OnHit;
        handler?.Invoke(this, e);
        hitCD.Start();
        DisableAim();
        DisableMov();

    }


    private void Update()
    {
        attackCD = PlayerGlobals.Instance.Cadence;

        disableTimer.Update();
        
        hitCD.Update();

        AttackCadence.Update();
        
    }

    public void DisableMov()
    {
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        disableTimer.Start();
    }


    public void DisableAim()
    {
        gameObject.GetComponent<PlayerAim>().canAttack = false;
        AttackCadence.Start();
    }

    private void ApplyFowardForce(object sender, EventArgs e)
    {
        Vector3 cameraPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = (cameraPos - transform.position);
        dir.z = 0;
        dir.Normalize();

        rb.velocity = Vector2.zero;
        rb.AddForce(dir * 0.25f, ForceMode2D.Impulse);
    }

    private void EnableMov(object sender, EventArgs e)
    {
        gameObject.GetComponent<PlayerMovement>().enabled = true;
        disableTimer.Stop();
    }

    private void EnableAim(object sender, EventArgs e)
    {
        gameObject.GetComponent<PlayerAim>().canAttack = true;
        AttackCadence.Stop();
    }

    public void ApplyKnockBack(object sender, EventArgs e)
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(direction * knockbackForce ,ForceMode2D.Impulse);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        HitScan(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        HitScan(collision);
    }


    private void HitScan(Collision2D collision)
    {
        if (hitCD.isActive)
        {
            return;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            direction = (transform.position - collision.transform.position).normalized;
            direction.z = 0;

            OnPlayerHitted(EventArgs.Empty);
        }
    }

    private void DropSaltpeter(object sender, EventArgs e)
    {
        int dropQ;

        if(PlayerGlobals.Instance.Saltpeter >= 2)
        {
            dropQ = 2;
        }
        else
        {
            dropQ = PlayerGlobals.Instance.Saltpeter;
        }

        for(int i = 0; i < dropQ;i++)
        {
            Instantiate(saltpeterDrop, transform.position, Quaternion.identity, null);
        }

        PlayerGlobals.Instance.DropSaltpeter(dropQ);

    }

}
