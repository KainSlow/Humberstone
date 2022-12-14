using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SaltpeterSound : MonoBehaviour
{
    SaltpeterBehavior sB;
    AudioSource aS;
    [SerializeField] List<AudioClip> HitSounds;

    private void Start()
    {
        sB = GetComponent<SaltpeterBehavior>();
        aS = GetComponent<AudioSource>();
        sB.OnHit += PlayHit;
    }

    private void PlayHit(object sender, EventArgs e)
    {
        aS.Stop();
        aS.volume = 0.6f;
        float pitch = UnityEngine.Random.Range(0.7f, 1f);
        aS.pitch = pitch;
        int rand = UnityEngine.Random.Range(0, HitSounds.Count);
        aS.PlayOneShot(HitSounds[rand]);
    }


}
