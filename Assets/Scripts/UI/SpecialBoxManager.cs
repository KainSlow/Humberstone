using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpecialBoxManager : MonoBehaviour
{
    Button GiveB;
    [SerializeField] Image[] ObjImages;
    [SerializeField] TextMeshProUGUI saltpeterNeeded;
    [SerializeField] GameObject popUp;
    [SerializeField] Button[] popUpButtons;

    int saltNeeded;

    bool needSalt;
    bool allObjCollected;

    void Start()
    {
        GiveB = GetComponentInChildren<Button>();
        GiveB.onClick.AddListener(Give);

        saltNeeded = PlayerGlobals.Instance.SaltpeterNeeded;
        saltpeterNeeded.text = saltNeeded.ToString();
        needSalt = true;

        popUpButtons[0].onClick.AddListener(Confirm);
        popUpButtons[1].onClick.AddListener(Cancel);
    }

    private void Update()
    {
        saltpeterNeeded.text = saltNeeded.ToString();
    }
    private void Confirm()
    {
        SceneManager.LoadScene("EndGame");
    }

    private void Cancel()
    {
        popUp.SetActive(false);
    }
    private void Give()
    {
        int objCollectedCounter = 0;

        for(int i = 0; i < 3; i++)
        {
            Color c = ObjImages[i].color;
            if (PlayerGlobals.Instance.isObjCollected[i])
            {
                ObjImages[i].color = new Color(c.r,c.g,c.b, 255f);
                objCollectedCounter++;
            }
        }

        if(objCollectedCounter == 3)
        {
            allObjCollected = true;
        }

        if(PlayerGlobals.Instance.Saltpeter >= saltNeeded)
        {
            PlayerGlobals.Instance.DecreaseSaltpeter(saltNeeded);
            PlayerGlobals.Instance.NeedNoSaltpeter();
            saltNeeded = 0;
            needSalt = false;
        }


        if(allObjCollected && !needSalt)
        {
            EndGame();
        }

    }


    private void EndGame()
    {
        popUp.GetComponentInChildren<TextMeshProUGUI>().text = "¿Terminar Juego?";
        popUp.SetActive(true);
    }


}
