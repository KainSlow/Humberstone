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

    [SerializeField] Image[] shopImages;
    int smallCost;
    int bigCost;

    string currenTitle;

    protected override void Start()
    {
        smallCost = 0;
        bigCost = 0;
        isInteracting = false;

        GetComponentInChildren<TextMeshPro>().text = Name;

        closeButton.onClick.AddListener(DeActivate);

        

    }

    public override void Interact()
    {

        if (!isInteracting)
        {
            isInteracting = true;
            currenTitle = titletxt;
            Shop.GetComponentInChildren<TextMeshProUGUI>().text = currenTitle;
            SetImages();
            SetPrices();
            Shop.SetActive(true);
            GameObject.Find("CameraHolder").GetComponent<CameraMov>().enabled = false;
            buyButtons[0].onClick.AddListener(SmallBuy);
            buyButtons[1].onClick.AddListener(BigBuy);

        }

    }

    private void SetImages()
    {
        if (currenTitle == "Herrería")
        {
            shopImages[0].sprite = Resources.Load<Sprite>("ShovelIco");
            shopImages[1].sprite = Resources.Load<Sprite>("BagIco");

        }
        else if (currenTitle == "Pulpería")
        {
            shopImages[0].sprite = Resources.Load<Sprite>("SmallFIco");
            shopImages[1].sprite = Resources.Load<Sprite>("BigFIco");
        }
    }

    private void SetPrices()
    {
        if (currenTitle == "Herrería")
        {
            smallCost = (int)(3 * PlayerGlobals.Instance.ShovelLVL * PlayerGlobals.Instance.Inflation);
            bigCost = (int)(7 * PlayerGlobals.Instance.BagLVL * PlayerGlobals.Instance.Inflation);
        }
        else if (currenTitle == "Pulpería")
        {
            smallCost = (int)(3 * PlayerGlobals.Instance.Inflation / PlayerGlobals.Instance.SuspicionLVL);
            bigCost = (int)(8 * PlayerGlobals.Instance.Inflation / PlayerGlobals.Instance.SuspicionLVL);
        }

        buyButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = smallCost.ToString();
        buyButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = bigCost.ToString();
    }

    private void SmallBuy()
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


    private void Buy(int cost)
    {
        PlayerGlobals.Instance.DecreaseTokens(cost);
        PlayerGlobals.Instance.OnItemBought(EventArgs.Empty);
        SetPrices(); 
    }

    private void BigBuy()
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
        buyButtons[0].onClick.RemoveAllListeners();
        buyButtons[1].onClick.RemoveAllListeners();

        isInteracting = false;
        Shop.SetActive(false);
    }
}
