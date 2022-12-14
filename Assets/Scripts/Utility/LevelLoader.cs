using System.Collections;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] Animator anim;

    public float transitionTime;
    public void LoadScene(string scene)
    {
        StartCoroutine(LoadLevel(scene));
    }

    IEnumerator LoadLevel(string scene)
    {
        anim.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        if (SceneManager.GetActiveScene().name == "WorkEnd" || SceneManager.GetActiveScene().name == "TownNight")
        {
            if(scene == "Town")
            {
                if (PlayerGlobals.Instance.SuspicionLVL >= PlayerGlobals.Instance.MaxSuspicion)
                {
                    PlayerGlobals.Instance.SetDefaultValues();
                    scene = "GameLost";
                }
                else
                {
                    PlayerGlobals.Instance.OnDayChanged(EventArgs.Empty);
                }
            }
            

        }

        SceneManager.LoadScene(scene);
    }
}
