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
    [SerializeField] TextMeshProUGUI totalSaltpeterTXT;
    [SerializeField] TextMeshProUGUI convertQTXT;
    [SerializeField] TextMeshProUGUI tokensTotal;

    private int currentQ;
    private int convertQ;
    private float totalTokens;

    LevelLoader ll;

    private void Start()
    {
        ll = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();


        maxButton.onClick.AddListener(MaxSaltpeter);
        minButton.onClick.AddListener(MinSaltpeter);

        addButton.onClick.AddListener(AddSaltpeter);
        substractButton.onClick.AddListener(SubSaltpeter);

        sleepButton.onClick.AddListener(BackToSleep);
        nightButton.onClick.AddListener(NightTown);


        fee.text = PlayerGlobals.Instance.DayFee.ToString();
        day.text = "Día:\n" + PlayerGlobals.Instance.Day;


        currentQ = PlayerGlobals.Instance.Saltpeter;
        convertQ = 0;
        tokensTotal.text = Convert().ToString("0.00");

    }

    private void Update()
    {
        totalSaltpeterTXT.text = currentQ.ToString();
        convertQTXT.text = convertQ.ToString();

        totalTokens = Convert();
        tokensTotal.text = totalTokens.ToString("0.00");

        if(convertQ < PlayerGlobals.Instance.DayFee)
        {
            convertQTXT.color = Color.red;
        }
        else
        {
            convertQTXT.color = Color.white;
        }

    }

    private float Convert()
    {
        return convertQ * PlayerGlobals.TCONSTANT;
    }

    private void MaxSaltpeter()
    {
        convertQ = PlayerGlobals.Instance.Saltpeter;
        currentQ = 0;
    }

    private void MinSaltpeter()
    {
        convertQ = 0;
        currentQ = PlayerGlobals.Instance.Saltpeter;
    }

    private void AddSaltpeter()
    {
        if (convertQ < PlayerGlobals.Instance.Saltpeter)
        {
            convertQ++;
            currentQ--;
        }
    }
    private void SubSaltpeter()
    {
        if (convertQ > 0)
        {
            convertQ--;
            currentQ++;
        }
    }


    private void BackToSleep()
    {
        Concrete();
        EndDay();
        ll.LoadScene("Town");
    }

    private void NightTown()
    {
        Concrete();
        PlayerGlobals.Instance.IncreaseSuspicion();
        ll.LoadScene("TownNight");

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
