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

    bool canAttack = true;

    Timer Cadence;


    private void Awake()
    {
        pM = GetComponent<PlayerManager>();
        Cadence = new Timer(0.45f);
        Cadence.OnTime += EnableAtt;
    }

    private void EnableAtt(object sender, EventArgs e)
    {
        canAttack = true;
    }

    private void Update()
    {
        Cadence.Update();
        HandleAim();

        if(canAttack) HandleShooting();
    }
    private void HandleAim()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        Vector3 aimDirection = mouseWorldPos - transform.position;
        angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        Aim.right = aimDirection;
    }

    private void HandleShooting()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject gm =Instantiate<GameObject>(Attack, Caster);
            gm.transform.eulerAngles = new Vector3(0f,0f,angle);
            canAttack = false;
            Cadence.Start();

            pM.OnMouseClicked(EventArgs.Empty);
            
        }
    }


}
