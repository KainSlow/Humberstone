using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyWManager : EnemyManager
{
    [SerializeField] float AttackCadence;
    public Timer AttackCD;
    public EventHandler OnAttack;
    public EventHandler OnCollect;

    private int saltPeterQ;
    [SerializeField] int dropQ;
    [SerializeField] GameObject saltPeterDrop;
    // Start is called before the first frame update

    public void OnDropCollected(EventArgs e)
    {
        EventHandler handler = OnCollect;
        handler?.Invoke(this,e);
    }
    public void OnAttackDone(EventArgs e)
    {
        EventHandler handler = OnAttack;
        handler?.Invoke(this, e);
    }

    protected override void Awake()
    {
        base.Awake();
        saltPeterQ = 0;

        AttackCD = new Timer(AttackCadence);
        OnAttack += DisableAttack;
        OnHit += SetAngry;
        OnCollect += AddSaltpeter;
        OnHit += DropSaltpeter;

        deathTimer.OnTime += DropAll;

    }

    private void DropAll(object sender, EventArgs e)
    {
        for(int i = 0; i < saltPeterQ; i++)
        {
            Instantiate(saltPeterDrop, transform.position, Quaternion.identity, null);
        }
    }

    private void DropSaltpeter(object sender, EventArgs e)
    {
        int drop = dropQ;

        if(saltPeterQ < drop)
        {
            drop = saltPeterQ;
        }

        for(int i = 0; i < drop; i++)
        {
            Instantiate(saltPeterDrop, transform.position, Quaternion.identity, null);
            saltPeterQ--;
        }

    }

    private void AddSaltpeter(object sender, EventArgs e)
    {
        saltPeterQ++;
    }

    private void SetAngry(object sender, EventArgs e)
    {
        GetComponent<EnemyWBehaviour>().isAngry = true;
    }



    private void DisableAttack(object sender, EventArgs e)
    {
        AttackCD.Start();
    }



    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        AttackCD.Update();
    }
}
