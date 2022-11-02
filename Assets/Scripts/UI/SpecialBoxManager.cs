using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialBoxManager : MonoBehaviour
{
    Button GiveB;
    [SerializeField] Image[] ObjImages;
    void Start()
    {
        GiveB = GetComponentInChildren<Button>();
        GiveB.onClick.AddListener(Give);
    }
    private void Give()
    {

        for(int i = 0; i < 3; i++)
        {
            Color c = ObjImages[i].color;

            if (PlayerGlobals.Instance.isObjCollected[i])
            {
                ObjImages[i].color = new Color(c.r,c.g,c.b, 255f);
            }
        }
    }

}
