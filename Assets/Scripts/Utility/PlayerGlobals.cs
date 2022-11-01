using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerGlobals
{
    private readonly static PlayerGlobals _instance = new PlayerGlobals();

    public EventHandler OnBuy;
    public EventHandler OnNewDay;
    public int ShovelLVL { get; private set; }
    public int BagLVL { get; private set; }
    public float Speed { get; private set; }
    public float Cadence { get; private set; }
    public float Tokens { get; private set; }
    public int Saltpeter { get; private set; }
    public float Hunger { get; private set; }
    public int maxSaltpeter { get; private set; }
    public float Inflation { get; private set; }

    public float maxDayTime { get; private set; }
    public float currentTime { get; private set; }
    public float Day { get; private set; }


    private PlayerGlobals()
    {
        SetDefaultValues();

        OnBuy += SetSpeed;
        OnBuy += SetCadence;
        OnBuy += SetMaxSaltpeter;

        OnNewDay += ReSetTime;
        OnNewDay += AddDay;
        OnNewDay += IncreaseHunger;

    }

    public static PlayerGlobals Instance
    {
        get
        {
            return _instance;
        }
    }

    public void OnItemBought(EventArgs e)
    {
        EventHandler handler = OnBuy;
        handler?.Invoke(this, e);
    }
    public void OnDayChanged(EventArgs e)
    {
        EventHandler handler = OnNewDay;
        handler?.Invoke(this, e);
    }

    private void SetDefaultValues()
    {
        maxDayTime = 60f;

        currentTime = maxDayTime;

        Day = 1f;
        Inflation = 1f;

        ShovelLVL = 1;
        BagLVL = 0;

        Hunger = 3;

        Tokens = 0f;
        Saltpeter = 0;

        Speed = Hunger * 0.25f + 0.25f;
        Cadence = 2f - 0.25f * ShovelLVL;
        maxSaltpeter = 10 + 5 * BagLVL;

    }

    private void IncreaseHunger(object sender, EventArgs e)
    {
        if(Hunger > 1)
        {
            Hunger--;
        }
    }

    private void FillHunger(object sender, EventArgs e)
    {

    }

    public void IncreaseTokens(float value)
    {
        Tokens += value;
    }

    public void DecreaseTokens(float value)
    {
        Tokens -= value;
    }

    #region Saltpeter Helpers
    public void AddSaltpeter()
    {
        Saltpeter++;
    }
    public void TransformSaltpeter(int Quantity)
    {
        //Transform to Tokens
        Tokens += Quantity * 3.5f;
        DropSaltpeter(Quantity);

    }
    public void DropSaltpeter(int value)
    {
        Saltpeter -= value;
    }
    #endregion

    #region Setters
    private void SetSpeed(object sender, EventArgs e)
    {
        Speed = Hunger * 0.25f + 0.25f;
    }
    private void SetCadence(object sender, EventArgs e)
    {
        Cadence = 2f - 0.25f * ShovelLVL;
    }
    private void SetMaxSaltpeter(object sender, EventArgs e)
    {
        maxSaltpeter = 10 + 5 * BagLVL;
    }

    private void ReSetTime(object sender, EventArgs e)
    {
        currentTime = maxDayTime;
    }

    private void AddDay(object sender, EventArgs e)
    {
        Day++;
    }

    #endregion


    public void UpdateTime()
    {
        currentTime -= Time.deltaTime;
    }

}
