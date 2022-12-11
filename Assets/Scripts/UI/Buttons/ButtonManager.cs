using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] Button[] buttons; // 0 -> PLay; 1 -> How to Play; 2-> Exit

    LevelLoader ll;

    // Start is called before the first frame update
    void Start()
    {
        buttons[0].onClick.AddListener(Play);
        //buttons[1].onClick.AddListener(HTP);
        buttons[2].onClick.AddListener(About);
        buttons[3].onClick.AddListener(Exit);

        ll = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();

    }

    private void Play()
    {
        ll.LoadScene("Town");
    }    
    private void HTP()
    {
        ll.LoadScene("Tutorial");
    }

    private void About()
    {
        ll.LoadScene("About");
    }

    private void Exit()
    {
        Application.Quit();
    }

}
