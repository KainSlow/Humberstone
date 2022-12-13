using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class NPCInteractable : MonoBehaviour
{
    [SerializeField] private GameObject DialogBox;
    [SerializeField] protected string Name;

    [SerializeField] private GameObject SpecialBox;

    TextMeshProUGUI[] TextMP;
    [SerializeField] TextAsset textData;
    protected bool isInteracting;

    public EventHandler OnInteract;

    protected virtual void Start()
    {
        if(DialogBox != null)
        {
            TextMP = DialogBox.GetComponentsInChildren<TextMeshProUGUI>();
        }
        isInteracting = false;
        GetComponentInChildren<TextMeshPro>().text = Name;
    }


    public virtual void Interact()
    {
        SetText();

        if (!isInteracting)
        {
            isInteracting = true;
            DialogBox.SetActive(true);


            if(Name == "Antonio")
            {
                SpecialBox.SetActive(true);
            }

            EventHandler handler = OnInteract;
            handler?.Invoke(this, EventArgs.Empty);
        }
    }

    private void SetText()
    {
        string[] txtData = textData.text.Split(new string[] {"\n" }, System.StringSplitOptions.None);
        TextMP[0].text = Name;

        int rand;
        
        rand = UnityEngine.Random.Range(0, txtData.Length);
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
