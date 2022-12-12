using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicFadeIn : MonoBehaviour
{
    bool isFadingIn;
    AudioSource aS;
    float maxVol;
    float volIncrement;

    [SerializeField] float fadeTime;

    void Start()
    {
        isFadingIn = true;
        aS = GetComponent<AudioSource>();
        maxVol = aS.volume;
        volIncrement = (float)maxVol / fadeTime;
        aS.volume = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFadingIn)
        {
            for(int i = 0; i < maxVol; i++)
            {
                aS.volume += volIncrement * Time.deltaTime;
            }

            if(aS.volume >= maxVol)
            {
                isFadingIn = false;
            }
        }
    }
}
