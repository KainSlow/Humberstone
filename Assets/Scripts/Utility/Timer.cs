using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    public event EventHandler OnTime;

    private float endTime;
    private float currentTime;

    private bool isLooping;
    public bool isActive { get; private set; }

    public Timer(float _endTime)
    {
        endTime = _endTime;
        isLooping = false;
    }

    public void SetCoolDown(float newValue)
    {
        endTime = newValue;
    }

    public void Start()
    {
        isActive = true;
        currentTime = 0;
    }

    public void Stop()
    {
        isActive = false;
        currentTime = 0;
    }

    public void Resume()
    {
        isActive = true;
    }

    public void Pause()
    {
        isActive = false;
    }

    public void Update()
    {
        if (isActive)
        {
            currentTime += Time.deltaTime;

            if(currentTime >= endTime)
            {
                currentTime -= currentTime;

                EventHandler handler = OnTime;

                handler?.Invoke(this, EventArgs.Empty);

                if (!isLooping)
                {
                    isActive = false;
                }
            }
        }
    }
}
