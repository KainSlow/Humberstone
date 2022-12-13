using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JavierBox : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Button button;
    [SerializeField] TextMeshProUGUI costTXT;

    int saltpeterCost;
    void Start()
    {
        saltpeterCost = 10;
        button.onClick.AddListener(Bribe);
    }

    private void Bribe()
    {
        if(PlayerGlobals.Instance.SuspicionLVL > 1)
        {
            if (PlayerGlobals.Instance.Saltpeter >= saltpeterCost)
            {
                PlayerGlobals.Instance.DecreaseSaltpeter(saltpeterCost);
                PlayerGlobals.Instance.DecreaseSuspicion();
            }
        }
    }
    void Update()
    {
        slider.value = (float)(PlayerGlobals.Instance.SuspicionLVL - 1) / (PlayerGlobals.Instance.MaxSuspicion - 1);
        costTXT.text = saltpeterCost.ToString();

    }
}
