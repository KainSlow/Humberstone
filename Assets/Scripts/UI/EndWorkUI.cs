using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class EndWorkUI : MonoBehaviour
{
    [SerializeField] Button maxButton;
    [SerializeField] Button minButton;
    [SerializeField] Button addButton;
    [SerializeField] Button substractButton;

    [SerializeField] Button sleepButton;
    [SerializeField] Button nightButton;

    [SerializeField] TextMeshProUGUI fee;
    [SerializeField] TextMeshProUGUI day;
    [SerializeField] TextMeshProUGUI saltpeterQ;
    [SerializeField] TextMeshProUGUI tokensTotal;

    private int convertQ;
    private float totalTokens;
    private void Start()
    {
        maxButton.onClick.AddListener(MaxSaltpeter);
        minButton.onClick.AddListener(MinSaltpeter);

        addButton.onClick.AddListener(AddSaltpeter);
        substractButton.onClick.AddListener(SubSaltpeter);

        sleepButton.onClick.AddListener(BackToSleep);
        nightButton.onClick.AddListener(NightTown);


        fee.text = PlayerGlobals.Instance.DayFee.ToString();
        day.text = "Día:\n" + PlayerGlobals.Instance.Day;

        convertQ = PlayerGlobals.Instance.Saltpeter;
        tokensTotal.text = Convert().ToString("0.00");

    }

    private void Update()
    {
        saltpeterQ.text = convertQ + "/" + PlayerGlobals.Instance.Saltpeter;
        totalTokens = Convert();
        tokensTotal.text = totalTokens.ToString("0.00");

        if(convertQ < PlayerGlobals.Instance.DayFee)
        {
            saltpeterQ.color = Color.red;
        }
        else
        {
            saltpeterQ.color = Color.white;
        }

    }

    private float Convert()
    {
        return convertQ * PlayerGlobals.TCONSTANT;
    }

    private void MaxSaltpeter()
    {
        convertQ = PlayerGlobals.Instance.Saltpeter;
    }

    private void MinSaltpeter()
    {
        convertQ = 0;
    }

    private void AddSaltpeter()
    {
        if (convertQ < PlayerGlobals.Instance.Saltpeter)
        {
            convertQ++;
        }
    }
    private void SubSaltpeter()
    {
        if (convertQ > 0)
        {
            convertQ--;
        }
    }


    private void BackToSleep()
    {
        Concrete();
        EndDay();
        SceneManager.LoadSceneAsync("Town");
    }

    private void NightTown()
    {
        Concrete();
        PlayerGlobals.Instance.IncreaseSuspicion();
        SceneManager.LoadSceneAsync("TownNight");

    }


    private void Concrete()
    {

        if(convertQ < PlayerGlobals.Instance.DayFee)
        {
            PlayerGlobals.Instance.IncreaseSuspicion();
        }
        else
        {
            PlayerGlobals.Instance.DecreaseSuspicion();
        }

        PlayerGlobals.Instance.IncreaseTokens(totalTokens);
        PlayerGlobals.Instance.DecreaseSaltpeter(convertQ);

    }

    private void EndDay()
    {
        PlayerGlobals.Instance.OnDayChanged(EventArgs.Empty);
    }

}
