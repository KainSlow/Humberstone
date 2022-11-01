using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoToMine : MonoBehaviour
{
    [SerializeField] GameObject PopUp;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PopUp.GetComponentInChildren<TextMeshProUGUI>().text = "Go to the Extraction Zone?";
        PopUp.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        PopUp.SetActive(false);

    }
}
