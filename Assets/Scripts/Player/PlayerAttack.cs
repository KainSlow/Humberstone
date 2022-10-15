using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Timer lifeSpan;

    private void Awake()
    {
        lifeSpan = new Timer(0.25f);
        lifeSpan.OnTime += Death;
        transform.parent = null;
    }

    private void Start()
    {
        lifeSpan.Start();
    }
    void Update()
    {
        lifeSpan.Update();
    }
    private void Death(object sender, EventArgs e)
    {
        Destroy(this.gameObject);
    }

}
