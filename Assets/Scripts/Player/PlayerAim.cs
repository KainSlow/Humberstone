using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    PlayerManager pM;
    [SerializeField] Transform Aim;
    [SerializeField] Transform Caster;
    [SerializeField] GameObject Attack;
    private float angle;
    public bool canAttack;


    private void Awake()
    {
        pM = GetComponent<PlayerManager>();
        canAttack = true;
    }

    private void Update()
    {
        HandleAim();

        if (canAttack)
        {
            HandleShooting();
        }
    }
    private void HandleAim()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        Vector3 aimDirection = mouseWorldPos - transform.position;
        angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        LimitAngle();

        Aim.eulerAngles = new Vector3(0f,0f,angle);

        //Aim.right = aimDirection;
    }

    private void HandleShooting()
    {
        if (Input.GetButton("Fire1"))
        {
            GameObject gm = Instantiate(Attack, Caster);
            gm.transform.eulerAngles = new Vector3(0f,0f,angle);

            gm.GetComponent<SpriteRenderer>().flipY = Caster.GetComponentInChildren<SpriteRenderer>().flipY;

            pM.OnMouseClicked(EventArgs.Empty);
            
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

}
