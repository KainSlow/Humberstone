using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySounds : MonoBehaviour
{
    EnemyManager eM;

    [SerializeField] AudioSource SFX;
    [SerializeField] List<AudioClip> HitSounds;

    void Start()
    {
        eM = GetComponentInParent<EnemyManager>();
        eM.OnHit += PlayHit;
    }

    public void PlayHit(object sender, EventArgs e)
    {
        if (!SFX.isPlaying)
        {
            float pitch = UnityEngine.Random.Range(0.8f, 1.2f);
            SFX.pitch = pitch;
            int rand = UnityEngine.Random.Range(0, HitSounds.Count);
            SFX.PlayOneShot(HitSounds[rand]);
        }
    }
}
