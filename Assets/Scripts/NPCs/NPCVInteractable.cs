using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEditor;
public class NPCVInteractable : NPCInteractable
{

    [SerializeField] string titletxt;
    [SerializeField] GameObject Shop;
    [SerializeField] Button closeButton;
    [SerializeField] Button[] buyButtons;
    [SerializeField] float IsmallCost;
    [SerializeField] float IbigCost;

    [SerializeField] TextMeshProUGUI smallText;
    [SerializeField] TextMeshProUGUI bigText;

    float smallCost;
    float bigCost;

    string currenTitle;

    protected override void Start()
    {
        isInteracting = false;

        GetComponentInChildren<TextMeshPro>().text = Name;

        closeButton.onClick.AddListener(DeActivate);
        buyButtons[0].onClick.AddListener(SmallBuy);
        buyButtons[1].onClick.AddListener(BigBuy);
    }

    public override void Interact()
    {

        if (!isInteracting)
        {
            isInteracting = true;
            currenTitle = titletxt;
            Shop.GetComponentInChildren<TextMeshProUGUI>().text = currenTitle;
            SetPrices();
            Shop.SetActive(true);
            GameObject.Find("CameraHolder").GetComponent<CameraMov>().enabled = false;
        }

    }

    private void SetPrices()
    {
        if (currenTitle == "Herrería")
        {
            smallCost = (IsmallCost * (PlayerGlobals.Instance.ShovelLVL * PlayerGlobals.Instance.Inflation));
            bigCost = (IbigCost * (PlayerGlobals.Instance.BagLVL * PlayerGlobals.Instance.Inflation));

            smallText.text = "Lvl: " + PlayerGlobals.Instance.ShovelLVL.ToString();
            bigText.text = "Lvl: "+ PlayerGlobals.Instance.BagLVL.ToString();

        }
        else if (currenTitle == "Pulpería")
        {
            smallCost = (IsmallCost * (PlayerGlobals.Instance.Inflation / PlayerGlobals.Instance.SuspicionLVL));
            bigCost = (IbigCost * (PlayerGlobals.Instance.Inflation / PlayerGlobals.Instance.SuspicionLVL));

            smallText.text = "Hallulla";
            bigText.text = "Sopa";

        }

        buyButtons[0].GetComponentInChildren<TextMeshProUGUI>(true).text = smallCost.ToString("0.00");
        buyButtons[1].GetComponentInChildren<TextMeshProUGUI>(true).text = bigCost.ToString("0.00");
    }

    public void SmallBuy()
    {
        Debug.Log(currenTitle);
        if(PlayerGlobals.Instance.Tokens >= smallCost)
        {
            if (currenTitle == "Herrería")
            {
                if (PlayerGlobals.Instance.ShovelLVL < PlayerGlobals.Instance.MaxShovelLVL)
                {
                    PlayerGlobals.Instance.BuyShovelLvl();
                    Buy(smallCost);
                }
               
            }
            else if (currenTitle == "Pulpería")
            {

                if (PlayerGlobals.Instance.Hunger < PlayerGlobals.Instance.MaxHunger)
                {
                    PlayerGlobals.Instance.BuyFood();
                    Buy(smallCost);
                }
            }
        }
    }


    private void Buy(float cost)
    {
        PlayerGlobals.Instance.DecreaseTokens(cost);
        PlayerGlobals.Instance.OnItemBought(EventArgs.Empty);
        SetPrices(); 
    }

    public void BigBuy()
    {
        if(PlayerGlobals.Instance.Tokens >= bigCost)
        {
            if (currenTitle == "Herrería")
            {
                if(PlayerGlobals.Instance.BagLVL < PlayerGlobals.Instance.MaxBagLVL)
                {
                    PlayerGlobals.Instance.BuyBagLVL();
                    Buy(bigCost);
                }
            }
            else if (currenTitle == "Pulpería")
            {
                if(PlayerGlobals.Instance.Hunger < PlayerGlobals.Instance.MaxHunger)
                {
                    PlayerGlobals.Instance.RefillFood();
                    Buy(bigCost);
                }
            }
        }
    }


    public override void DeActivate()
    {
        isInteracting = false;
        Shop.SetActive(false);
    }
}
