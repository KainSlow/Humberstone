using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerGlobals
{
    private readonly static PlayerGlobals _instance = new PlayerGlobals();

    public EventHandler OnBuy;
    public EventHandler OnNewDay;

    public const float TCONSTANT = 1.5f;

    public bool hasSeenFighting { get; private set; }
    public int ShovelLVL { get; private set; }
    public int MaxShovelLVL { get; private set; }
    public int BagLVL { get; private set; }
    public int MaxBagLVL { get; private set; }
    public float Speed { get; private set; }
    public float Cadence { get; private set; }
    public float Tokens { get; private set; }
    public int Saltpeter { get; private set; }
    public float Hunger { get; private set; }
    public int MaxHunger {get;private set;}
    public int maxSaltpeter { get; private set; }
    public float Inflation { get; private set; }
    public float SuspicionLVL { get; private set; }
    public float MaxSuspicion { get; private set; }
    public float maxDayTime { get; private set; }
    public float currentTime { get; private set; }
    public int Day { get; private set; }
    public int DayFee { get; private set; }

    public int SaltpeterNeeded { get; private set; }

    public bool[] isObjCollected;

    private PlayerGlobals()
    {
        SetDefaultValues();

        OnBuy += SetSpeed;
        OnBuy += SetCadence;
        OnBuy += SetMaxSaltpeter;

        OnNewDay += SetSpeed;
        OnNewDay += AddDay;
        OnNewDay += IncreaseHunger;
        OnNewDay += ReSetTime;
        OnNewDay += IncreaseInflation;
        OnNewDay += IncreaseFee;
        OnNewDay += ResetSeenFighting;
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

    public void SetDefaultValues()
    {
        //Debug

        isObjCollected = new bool[3];

        SaltpeterNeeded = 50;

        maxDayTime = 60f;

        currentTime = maxDayTime;

        Day = 1;

        DayFee = 0;
        Inflation = 1f;
        SuspicionLVL = 1f;
        MaxSuspicion = 4f;

        MaxShovelLVL = 7;
        MaxBagLVL = 10;
        ShovelLVL = 1;
        BagLVL = 1;

        Hunger = 3;
        MaxHunger = 6;

        Tokens = 12;
        Saltpeter = 0;

        Speed = Hunger * 0.25f + 0.25f;
        Cadence = 2f - 0.25f * ShovelLVL;
        maxSaltpeter = 10 + 5 * BagLVL;

    }

    public void SeenFighting()
    {
        hasSeenFighting = true;
    }

    private void ResetSeenFighting(object sender, EventArgs e)
    {
        if (hasSeenFighting)
        {
            SuspicionLVL++;
            hasSeenFighting = false;
        }
    }

    public void SetObjCollected(int objType)
    {
        isObjCollected[objType] = true;
    }

    public void NeedNoSaltpeter()
    {
        SaltpeterNeeded = 0;
    }

    private void IncreaseHunger(object sender, EventArgs e)
    {
        if(Hunger > 1)
        {
            Hunger--;
        }
    }
    private void IncreaseFee(object sender, EventArgs e)
    {
        DayFee = (int)(Day/2) * 3;
    }

    private void IncreaseInflation(object sender, EventArgs e)
    {
        Inflation *= 1.08f;
    }

    public void IncreaseSuspicion()
    {
        SuspicionLVL++;
    }

    public void DecreaseSuspicion()
    {
        if(SuspicionLVL > 1)
        {
            SuspicionLVL--;
        }
    }

    public void BuyShovelLvl()
    {
        ShovelLVL++;
    }
    public void BuyBagLVL()
    {
        BagLVL++;
    }


    public void BuyFood()
    {
        Hunger++;
    }

    public void RefillFood()
    {
        Hunger = MaxHunger;
    }


    public void IncreaseTokens(float value)
    {
        Tokens += value;
    }

    public void DecreaseTokens(float value)
    {
        Tokens -= value;
    }

    public void DecreaseSaltpeter(int value)
    {
        Saltpeter -= value;
    }

    #region Saltpeter Helpers
    public void AddSaltpeter()
    {
        Saltpeter++;
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
        maxSaltpeter = 15 + 10 * BagLVL;
    }

    private void ReSetTime(object sender, EventArgs e)
    {
        currentTime = maxDayTime;
    }


    public void SetNightTime()
    {
        currentTime = 0f;
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
