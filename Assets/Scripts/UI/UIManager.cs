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
    [SerializeField] Image InteractKey;
    [SerializeField] Slider cdSlide;
    TextMeshProUGUI[] textMP;

    public bool canInteract;
    Slider hungerSlide;
    LevelLoader ll;

    void Start()
    {

        ll = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();


        textMP = GetComponentsInChildren<TextMeshProUGUI>();
        hungerSlide = GetComponentInChildren<Slider>();

        endWorkButton.onClick.AddListener(PopUp);
        popUpButtons[0].onClick.AddListener(Confirm);
        popUpButtons[1].onClick.AddListener(Cancel);

        if (SceneManager.GetActiveScene().name == "CaveZone")
        {
            endWorkButton.gameObject.SetActive(true);
            popUp.GetComponentInChildren<TextMeshProUGUI>().text = "Estas seguro/a?";
        }

        Debug.Log(PlayerGlobals.Instance.SuspicionLVL);


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
        textMP[3].text = "Dia: " + PlayerGlobals.Instance.Day.ToString();


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

        if (canInteract)
        {
            InteractKey.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(oPlayer.transform.position - Vector3.up * 0.7f);
            InteractKey.gameObject.SetActive(true);
        }
        else
        {
            InteractKey.gameObject.SetActive(false);

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
                ll.LoadScene("CaveZone");
            }
            else if(currentScene.name == "CaveZone")
            {
                ll.LoadScene("WorkEnd");
            }else if(currentScene.name == "Tutorial")
            {
                ll.LoadScene("MenuScene");
            }
        }
        else
        {
            

            ll.LoadScene("Town");
        }
    }

    private void Cancel()
    {
        Time.timeScale = 1f;
        popUp.SetActive(false);
    }


}
