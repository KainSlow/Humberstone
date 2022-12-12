using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class NPCSoundManager : MonoBehaviour
{
    AudioSource sfx;
    [SerializeField] List<AudioClip> audioClips;
    [SerializeField] AudioClip buyClip;
    NPCInteractable manager;
    NPCVInteractable vManager;
    private void Start()
    {
        sfx = GetComponent<AudioSource>();
        manager = GetComponent<NPCInteractable>();
        manager.OnInteract += PlayInteract;

        vManager = GetComponent<NPCVInteractable>();
        if(vManager != null)
        {
            vManager.OnBuy += PlayBuy;
        }
    }

    private void PlayInteract(object sender, EventArgs e)
    {
        if (!sfx.isPlaying)
        {
            float pitch = UnityEngine.Random.Range(0.9f, 1f);
            sfx.pitch = pitch;
            int rand = UnityEngine.Random.Range(0, audioClips.Count);
            if(audioClips.Count > 0)
            {
                sfx.PlayOneShot(audioClips[rand]);
            }
        }
    }

    private void PlayBuy(object sender, EventArgs e)
    {
        sfx.PlayOneShot(buyClip);
    }

}
