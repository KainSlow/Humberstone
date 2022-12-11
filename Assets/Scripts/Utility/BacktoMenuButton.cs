using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BacktoMenuButton : MonoBehaviour
{
    LevelLoader ll;

    // Start is called before the first frame update
    void Start()
    {
        ll = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();

        GetComponent<Button>().onClick.AddListener(GoMenu);
    }

    private void GoMenu()
    {
        if(SceneManager.GetActiveScene().name == "GameLost")
        {
            PlayerGlobals.Instance.SetDefaultValues();
        }
        ll.LoadScene("MenuScene");
    }
}
