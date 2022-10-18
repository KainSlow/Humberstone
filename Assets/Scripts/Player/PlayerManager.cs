using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public event EventHandler OnClick;
    public event EventHandler OnHit;

    Rigidbody2D rb;

    Timer disableTimer;
    bool isPlayerD;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        disableTimer = new Timer(0.3f);
        disableTimer.OnTime += EnablePlayer;
    }

    public virtual void OnMouseClicked(EventArgs e)
    {
        EventHandler handler = OnClick;
        handler?.Invoke(this, e);
    }


    private void Update()
    {
        if (isPlayerD)
        {
            disableTimer.Update();
        }
    }

    public void DisableMov()
    {
        isPlayerD = true;
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        gameObject.GetComponent<PlayerAim>().enabled = false;
        disableTimer.Start();

        Vector3 cameraPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = (cameraPos - transform.position);
        dir.z = 0;
        dir.Normalize();
        Debug.Log(dir);

        rb.velocity = Vector2.zero;
        rb.AddForce(dir * 0.25f, ForceMode2D.Impulse);

    }

    private void EnablePlayer(object sender, EventArgs e)
    {
        isPlayerD = false;
        gameObject.GetComponent<PlayerMovement>().enabled = true;
        gameObject.GetComponent<PlayerAim>().enabled = true;
        disableTimer.Stop();
    }


    private void ApplyKnockBack(Vector2 direction)
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(direction,ForceMode2D.Impulse);

    }

}
