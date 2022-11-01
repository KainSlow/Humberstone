using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [SerializeField] Button endWorkButton;
    [SerializeField] GameObject popUp;
    [SerializeField] Button[] popUpButtons;

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
            popUp.GetComponentInChildren<TextMeshProUGUI>().text = "Are you sure?";
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
        hungerSlide.value = PlayerGlobals.Instance.Hunger / 5f;
        textMP[1].text = PlayerGlobals.Instance.Tokens.ToString("0.0");
        textMP[3].text = "Day: " + PlayerGlobals.Instance.Day.ToString();
    }


    private void PopUp()
    {
        popUp.SetActive(true);
        Time.timeScale = 0f;
    }

    private void Confirm()
    {
        Time.timeScale = 1f;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void Cancel()
    {
        Time.timeScale = 1f;
        popUp.SetActive(false);
    }


}
