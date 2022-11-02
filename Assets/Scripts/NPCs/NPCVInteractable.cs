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

        buyButtons[0].onClick.AddListener(SmallBuy);
        buyButtons[1].onClick.AddListener(BigBuy);

    }

    public override void Interact()
    {

        if (!isInteracting)
        {
            isInteracting = true;
            Shop.GetComponentInChildren<TextMeshProUGUI>().text = titletxt;

            currenTitle = Shop.GetComponentInChildren<TextMeshProUGUI>().text;


            SetImages();
            SetPrices();
            Shop.SetActive(true);

            GameObject.Find("CameraHolder").GetComponent<CameraMov>().enabled = false;

        }
    }

    private void SetImages()
    {
        if (currenTitle == "Herrer�a")
        {

            shopImages[0].sprite = Resources.Load<Sprite>("ShovelIco");
            shopImages[1].sprite = Resources.Load<Sprite>("BagIco");

        }
        else if (currenTitle == "Pulper�a")
        {
            shopImages[0].sprite = Resources.Load<Sprite>("SmallFIco");
            shopImages[1].sprite = Resources.Load<Sprite>("BigFIco");
        }
    }

    private void SetPrices()
    {
        if (currenTitle == "Herrer�a")
        {

            smallCost = (int)(10 * PlayerGlobals.Instance.ShovelLVL * PlayerGlobals.Instance.Inflation);
            bigCost = (int)(20 * PlayerGlobals.Instance.BagLVL * PlayerGlobals.Instance.Inflation);
        }
        else if (currenTitle == "Pulper�a")
        {
            smallCost = (int)(3 * PlayerGlobals.Instance.Inflation / PlayerGlobals.Instance.SuspicionLVL);
            bigCost = (int)(8 * PlayerGlobals.Instance.Inflation / PlayerGlobals.Instance.SuspicionLVL);
        }

        buyButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = smallCost.ToString();
        buyButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = bigCost.ToString();
    }

    private void SmallBuy()
    {
        if(PlayerGlobals.Instance.Tokens >= smallCost)
        {
            if (currenTitle == "Herrer�a")
            {


                if (PlayerGlobals.Instance.ShovelLVL < PlayerGlobals.Instance.MaxShovelLVL)
                {
                    PlayerGlobals.Instance.BuyShovelLvl();
                    Buy(smallCost);
                }
               
            }
            else if (currenTitle == "Pulper�a")
            {

                if (PlayerGlobals.Instance.Hunger < 5)
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
            if (currenTitle == "Herrer�a")
            {
                if(PlayerGlobals.Instance.BagLVL < PlayerGlobals.Instance.MaxBagLVL)
                {
                    PlayerGlobals.Instance.BuyBagLVL();
                    Buy(bigCost);
                }
            }
            else if (currenTitle == "Pulper�a")
            {
                if(PlayerGlobals.Instance.Hunger < 5)
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
