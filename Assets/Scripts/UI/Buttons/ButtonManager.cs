using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] Button[] buttons; // 0 -> PLay; 1 -> How to Play; 2-> Exit

    // Start is called before the first frame update
    void Start()
    {
        buttons[0].onClick.AddListener(Play);
        buttons[1].onClick.AddListener(HTP);
        buttons[2].onClick.AddListener(HTP);
        buttons[3].onClick.AddListener(Exit);
    }

    private void Play()
    {
        SceneManager.LoadScene("Town");
    }    
    private void HTP()
    {
        SceneManager.LoadScene("Tutorial");
    }

    private void About()
    {
        SceneManager.LoadScene("About");
    }

    private void Exit()
    {
        Application.Quit();
    }

}
