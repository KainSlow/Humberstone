using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    public event EventHandler OnTime;

    public float EndTime { get; private set; }
    public float CurrentTime { get; private set; }

    private bool isLooping;
    public bool isActive { get; private set; }

    public Timer(float _endTime)
    {
        EndTime = _endTime;
        isLooping = false;
    }

    public void SetCoolDown(float newValue)
    {
        EndTime = newValue;
    }

    public void ActivateLooping()
    {
        isLooping = true;
    }

    public void DeActivateLooping()
    {
        isLooping = false;
    }

    public void Start()
    {
        isActive = true;
        CurrentTime = 0;
    }

    public void Stop()
    {
        isActive = false;
        CurrentTime = 0;
    }

    public void Stop(bool isCalling)
    {
        isActive = false;
        CurrentTime = 0;

        if (isCalling)
        {
            CallEvent();
        }

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
            CurrentTime += Time.deltaTime;

            if(CurrentTime >= EndTime)
            {
                CurrentTime -= CurrentTime;

                CallEvent();

                if (!isLooping)
                {
                    isActive = false;
                }
            }
        }
    }

    private void CallEvent()
    {
        EventHandler handler = OnTime;

        handler?.Invoke(this, EventArgs.Empty);
    }

}
