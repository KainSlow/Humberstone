using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClockManager : MonoBehaviour
{
    [SerializeField] RectTransform secondHand;
    TextMeshProUGUI time;

    private float Speed;
    private float currentAngle;

    Scene currentScene;

    LevelLoader ll;
    void Start()
    {
        ll = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();

        time = GetComponentInChildren<TextMeshProUGUI>();
        currentScene = SceneManager.GetActiveScene();
        Speed = 180f / PlayerGlobals.Instance.maxDayTime;
        currentAngle = 0f;

        if (currentScene.name == "TownNight")
        {
            secondHand.transform.eulerAngles = new Vector3(0f, 0f, 180f);
            PlayerGlobals.Instance.SetNightTime();
        }

    }

    // Update is called once per frame
    void Update()
    {
        time.text = (PlayerGlobals.Instance.currentTime).ToString("0") + "s";

        if (currentScene.name == "CaveZone")
        {

            if (PlayerGlobals.Instance.currentTime > 0)
            {
                PlayerGlobals.Instance.UpdateTime();
                MoveSecondHand();
            }
            else
            {
                ll.LoadScene("WorkEnd");
            }

        }
        
    }

    private void MoveSecondHand()
    {
        currentAngle -= Speed * Time.deltaTime;
        secondHand.eulerAngles = new Vector3(0f, 0f, currentAngle);
    }
}
