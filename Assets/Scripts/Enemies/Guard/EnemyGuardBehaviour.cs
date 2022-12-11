using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyGuardBehaviour : EnemyBehavior
{
    Vector3 originPos;
    bool areFighting;
    TextMeshPro text;
    LineRenderer lR;


    protected override void Start()
    {
        text = GetComponentInChildren<TextMeshPro>();
        base.Start();
        originPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
            
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
