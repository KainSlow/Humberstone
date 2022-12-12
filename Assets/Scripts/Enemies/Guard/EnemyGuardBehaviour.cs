using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class EnemyGuardBehaviour : EnemyBehavior
{
    Vector3 originPos;
    bool areFighting;
    TextMeshPro text;
    LineRenderer lR;

    [SerializeField] GameObject Aim;

    protected override void Start()
    {
        text = GetComponentInChildren<TextMeshPro>();
        base.Start();
        originPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, 1.5f);

        for(int i=0;i< col.Length; i++)
        {
            if(col[i] != null)
            {
                if (col[i].CompareTag("Player"))
                {
                    Debug.Log("Funciono");
                    Attack();
                }
            }
        }


        SetAimAngle();
    }

    private void SetAimAngle()
    {
        Vector3 dir = (player.transform.position - transform.position).normalized;

        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;

        angle = LimitAngle(angle);

        Aim.transform.eulerAngles = new(0f,0f,angle);
    }

    private float LimitAngle(float angle)
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

        return angle;
    }

    private void Attack()
    {
        GetComponent<EnemyGuardManager>().OnAttacked(EventArgs.Empty);
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack"))
        {
            return;
        }

        EnemyWBehaviour eWB = collision.gameObject.GetComponentInParent<EnemyWBehaviour>();
        PlayerManager pM = collision.gameObject.GetComponentInParent<PlayerManager>();

        if(eWB != null)
        {
            if (eWB.isAngry)
            {
                areFighting = true;
            }
            else
            {
                SetYellow();
            }

        }
        else if(pM != null && areFighting)
        {
            text.text = "¡No peleen aquí!";
            SetRed();
            PlayerGlobals.Instance.SeenFighting();

        }else if(pM != null)
        {
            text.text = "¡Trabaja!";
            SetYellow();
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack"))
        {
            return;
        }

        EnemyWBehaviour eWB = collision.gameObject.GetComponentInParent<EnemyWBehaviour>();
        PlayerManager pM = collision.gameObject.GetComponentInParent<PlayerManager>();

        if (eWB != null || pM != null)
        {
            SetYellow();
            areFighting = false;
            text.text = "";
        }

    }

    private void SetRed()
    {
        lR = GetComponentInChildren<LineRenderer>();
        lR.startColor = Color.red;
        lR.endColor = Color.red;
    }

    private void SetYellow()
    {
        lR = GetComponentInChildren<LineRenderer>();
        lR.startColor = Color.yellow;
        lR.endColor = Color.yellow;
    }
}
