using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class UIManager : MonoBehaviour
{
    [SerializeField] Button endWorkButton;
    [SerializeField] GameObject popUp;
    [SerializeField] Button[] popUpButtons;

    [SerializeField] Slider cdSlide;

    TextMeshProUGUI[] textMP;
    Slider hungerSlide;
    void Start()
    {
        textMP = GetComponentsInChildren<TextMeshProUGUI>();
        hungerSlide = GetComponentInChildren<Slider>();

        endWorkButton.onClick.AddListener(PopUp);
        popUpButtons[0].onClick.AddListener(Confirm);
        popUpButtons[1].onClick.AddListener(Cancel);

        if (SceneManager.GetActiveScene().name == "CaveZone")
        {
            endWorkButton.gameObject.SetActive(true);
            popUp.GetComponentInChildren<TextMeshProUGUI>().text = "¿Estás seguro/a?";
        }


        Debug.Log(PlayerGlobals.Instance.SuspicionLVL);

        if(PlayerGlobals.Instance.SuspicionLVL >= PlayerGlobals.Instance.MaxSuspicion && SceneManager.GetActiveScene().name == "Town")
        {
            PlayerGlobals.Instance.SetDefaultValues();
            SceneManager.LoadSceneAsync("GameLost");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerGlobals.Instance.Saltpeter == PlayerGlobals.Instance.maxSaltpeter)
        {
            textMP[0].color = Color.cyan;
        }
        else
        {
            textMP[0].color = Color.white;

        }

        textMP[0].text = PlayerGlobals.Instance.Saltpeter + "/" + PlayerGlobals.Instance.maxSaltpeter;
        hungerSlide.value = PlayerGlobals.Instance.Hunger / PlayerGlobals.Instance.MaxHunger;
        textMP[1].text = PlayerGlobals.Instance.Tokens.ToString("0.0");
        textMP[3].text = "Día: " + PlayerGlobals.Instance.Day.ToString();


        GameObject oPlayer = GameObject.Find("Player");
        PlayerManager pM = oPlayer.GetComponent<PlayerManager>();

        if (pM.AttackCadence.isActive)
        {
            cdSlide.gameObject.SetActive(true);
            cdSlide.value = pM.AttackCadence.CurrentTime / pM.AttackCadence.EndTime;
            cdSlide.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(oPlayer.transform.position + Vector3.down * 0.6f);
        }
        else
        {
            cdSlide.gameObject.SetActive(false);
        }


    }


    private void PopUp()
    {
        popUp.SetActive(true);
        Time.timeScale = 0f;
    }

    private void Confirm()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        Time.timeScale = 1f;
        if(currentScene.name != "TownNight")
        {
            if(currentScene.name == "Town")
            {
                SceneManager.LoadSceneAsync("CaveZone");
            }
            else if(currentScene.name == "CaveZone")
            {
                SceneManager.LoadSceneAsync("WorkEnd");
            }
        }
        else
        {
            SceneManager.LoadSceneAsync("Town");
            PlayerGlobals.Instance.OnDayChanged(EventArgs.Empty);
        }
    }

    private void Cancel()
    {
        Time.timeScale = 1f;
        popUp.SetActive(false);
    }


}
