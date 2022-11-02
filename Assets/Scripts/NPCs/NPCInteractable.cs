using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCInteractable : MonoBehaviour
{
    [SerializeField] private GameObject DialogBox;
    [SerializeField] protected string Name;

    [SerializeField] private GameObject SpecialBox;

    TextMeshProUGUI[] TextMP;
    [SerializeField] TextAsset textData;

    protected bool isInteracting;

    protected virtual void Start()
    {
        TextMP = DialogBox.GetComponentsInChildren<TextMeshProUGUI>();
        isInteracting = false;
        GetComponentInChildren<TextMeshPro>().text = Name;
    }


    public virtual void Interact()
    {
        if (!isInteracting)
        {
            isInteracting = true;
            DialogBox.SetActive(true);

            if(Name == "Antonio")
            {
                SpecialBox.SetActive(true);
            }

        }
        SetText();

    }

    private void SetText()
    {
        string[] txtData = textData.text.Split(new string[] {"\n" }, System.StringSplitOptions.None);
        TextMP[0].text = Name;

        int rand;
        
        rand = Random.Range(0, txtData.Length);
        TextMP[1].text = txtData[rand];

        if (txtData[rand].Equals("") || txtData[rand].Equals(" ") || txtData[rand].Equals("\n") || txtData[rand] == null)
        {
            SetText();
        }

    }


    public virtual void DeActivate()
    {
        isInteracting = false;
        DialogBox.SetActive(false);

        if (Name == "Antonio")
        {
            SpecialBox.SetActive(false);
        }
    }



}
