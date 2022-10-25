using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public event EventHandler OnClick;
    public event EventHandler OnHit;

    [SerializeField] float attackCD;
    [SerializeField] float knockbackDuration;
    [SerializeField] float knockbackForce;

    Rigidbody2D rb;

    Timer disableTimer;
    Timer hitCD;

    private Vector3 direction;
    private void Awake()
    {
        OnClick += ApplyFowardForce;
        OnHit += ApplyKnockBack;

        rb = GetComponent<Rigidbody2D>();
        disableTimer = new Timer(attackCD);
        disableTimer.OnTime += EnablePlayer;
        hitCD = new Timer(knockbackDuration);
        hitCD.OnTime += EnablePlayer;
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
    }


    private void Update()
    {
        Debug.Log(disableTimer.CurrentTime);
        Debug.Log(GetComponent<PlayerAim>().enabled);
        disableTimer.Update();
        
        hitCD.Update();
        
    }

    public void DisableMov()
    {
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        gameObject.GetComponent<PlayerAim>().enabled = false;
        disableTimer.Start();
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

    private void EnablePlayer(object sender, EventArgs e)
    {
        gameObject.GetComponent<PlayerMovement>().enabled = true;
        gameObject.GetComponent<PlayerAim>().enabled = true;
        disableTimer.Stop();
    }


    public void ApplyKnockBack(object sender, EventArgs e)
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(direction * knockbackForce ,ForceMode2D.Impulse);
    }


    private void OnCollisionEnter2D(Collision2D collision)
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

}
