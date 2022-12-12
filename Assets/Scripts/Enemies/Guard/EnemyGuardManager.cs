using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGuardManager : EnemyManager
{
    public EventHandler OnAttack;
    [SerializeField] GameObject oAttack;
    [SerializeField] GameObject oCaster;
    [SerializeField] GameObject oAim;
    [SerializeField] float AttackCd;
    Timer AttackTimer;

    void Start()
    {
        OnAttack += Attack;
        AttackTimer = new Timer(AttackCd);
    }

    public void Attack(object sender, EventArgs e)
    {
        Instantiate(oAttack, oCaster.transform);
        AttackTimer.Start();
    }

    public void OnAttacked(EventArgs e)
    {
        if (!AttackTimer.isActive)
        {
            EventHandler handler = OnAttack;
            handler?.Invoke(this, e);
        }
    }


    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        AttackTimer.Update();
    }


    protected override void Death(object sender, EventArgs e)
    {
        
    }
}
