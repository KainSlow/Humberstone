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

    public EventHandler OnBuy;


    protected override void Start()
    {
        isInteracting = false;

        GetComponentInChildren<TextMeshPro>().text = Name;

        if(closeButton != null)
        {
            closeButton.onClick.AddListener(DeActivate);
        }

        if(buyButtons[0] != null)
        {
            buyButtons[0].onClick.AddListener(SmallBuy);
            buyButtons[1].onClick.AddListener(BigBuy);
        }

    }

    public void OnBought(EventArgs e)
    {
        EventHandler handler = OnBuy;
        handler?.Invoke(this, e);
    }

    public override void Interact()
    {

        if (!isInteracting)
        {

            EventHandler handler = OnInteract;
            handler?.Invoke(this, EventArgs.Empty);

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
        if (currenTitle == "Herreria")
        {
            if(PlayerGlobals.Instance.ShovelLVL < PlayerGlobals.Instance.MaxShovelLVL)
            {
                smallText.text = "Lvl: " + PlayerGlobals.Instance.ShovelLVL.ToString();
                smallCost = (IsmallCost * (PlayerGlobals.Instance.ShovelLVL * PlayerGlobals.Instance.Inflation));

                buyButtons[0].GetComponentInChildren<TextMeshProUGUI>(true).text = smallCost.ToString("0.00");

            }
            else
            {
                smallText.text = "Lvl: MAX";
                buyButtons[0].GetComponentInChildren<TextMeshProUGUI>(true).text = "-/-";

            }

            if (PlayerGlobals.Instance.BagLVL < PlayerGlobals.Instance.MaxBagLVL)
            {
                bigText.text = "Lvl: " + PlayerGlobals.Instance.BagLVL.ToString();
                bigCost = (IbigCost * (PlayerGlobals.Instance.BagLVL * PlayerGlobals.Instance.Inflation));


                buyButtons[1].GetComponentInChildren<TextMeshProUGUI>(true).text = bigCost.ToString("0.00");

            }
            else
            {
                bigText.text = "Lvl: MAX";
                buyButtons[1].GetComponentInChildren<TextMeshProUGUI>(true).text = "-/-";

            }




        }
        else if (currenTitle == "Pulperia")
        {
            smallCost = (IsmallCost * (PlayerGlobals.Instance.Inflation * PlayerGlobals.Instance.SuspicionLVL*0.5f));
            bigCost = (IbigCost * (PlayerGlobals.Instance.Inflation * PlayerGlobals.Instance.SuspicionLVL*0.5f));

            smallText.text = "Hallulla";
            bigText.text = "Sopa";


            buyButtons[0].GetComponentInChildren<TextMeshProUGUI>(true).text = smallCost.ToString("0.00");
            buyButtons[1].GetComponentInChildren<TextMeshProUGUI>(true).text = bigCost.ToString("0.00");

        }

        
    }

    public void SmallBuy()
    {
        Debug.Log(currenTitle);
        if(PlayerGlobals.Instance.Tokens >= smallCost)
        {
            if (currenTitle == "Herreria")
            {
                if (PlayerGlobals.Instance.ShovelLVL < PlayerGlobals.Instance.MaxShovelLVL)
                {
                    PlayerGlobals.Instance.BuyShovelLvl();
                    Buy(smallCost);
                }
               
            }
            else if (currenTitle == "Pulperia")
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
        OnBought(EventArgs.Empty);
        PlayerGlobals.Instance.DecreaseTokens(cost);
        PlayerGlobals.Instance.OnItemBought(EventArgs.Empty);
        SetPrices(); 
    }

    public void BigBuy()
    {
        if(PlayerGlobals.Instance.Tokens >= bigCost)
        {
            if (currenTitle == "Herreria")
            {
                if(PlayerGlobals.Instance.BagLVL < PlayerGlobals.Instance.MaxBagLVL)
                {
                    PlayerGlobals.Instance.BuyBagLVL();
                    Buy(bigCost);
                }
            }
            else if (currenTitle == "Pulperia")
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
