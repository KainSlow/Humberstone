using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCInteractable : MonoBehaviour
{
    [SerializeField] GameObject DialogBox;
    [SerializeField] string Name;
    CanvasRenderer CR;
    TextMeshProUGUI[] TextMP;

    [SerializeField] TextAsset textData;

    bool isInteracting;

    private void Start()
    {

        TextMP = DialogBox.GetComponentsInChildren<TextMeshProUGUI>();
        isInteracting = false;
        CR = DialogBox.GetComponent<CanvasRenderer>();
        GetComponentInChildren<TextMeshPro>().text = Name;

    }


    public virtual void Interact()
    {
        if (!isInteracting)
        {
            isInteracting = true;
            DialogBox.SetActive(true);

        }
        SetText();

    }

    private void SetText()
    {
        string[] txtData = textData.text.Split(new string[] { ",", "\n" }, System.StringSplitOptions.None);

        int rand = Random.Range(0, txtData.Length);

        TextMP[0].text = Name;
        TextMP[1].text = txtData[rand];


        if (txtData[rand].Equals(""))
        {
            SetText();
        }

    }


    public void DeActivate()
    {
        isInteracting = false;
        DialogBox.SetActive(false);
    }



}
