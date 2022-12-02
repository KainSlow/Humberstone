using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAudio : MonoBehaviour
{

    PlayerManager pM;
    [SerializeField] AudioSource movSrc;
    [SerializeField] AudioSource SFX;

    [SerializeField] AudioClip Attack;
    [SerializeField] AudioClip Walk;
    [SerializeField] AudioClip Hit;

    private void Start()
    {
        pM = GetComponentInParent<PlayerManager>();

        pM.OnClick += PlayAttack;
        pM.OnHit += PlayHit;
    }

    private void Update()
    {
        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            PlayWalk();
        }
        else
        {
            movSrc.Stop();
        }

    }

    public void PlayHit(object sender, EventArgs e)
    {
        SFX.volume = 0.3f;
        SFX.pitch = 1f;
        SFX.PlayOneShot(Hit, 1f);
    }

    public void PlayWalk()
    {
        if (!movSrc.isPlaying)
        {
            movSrc.pitch = PlayerGlobals.Instance.Speed* 1.2f;
            movSrc.PlayOneShot(Walk, 0.4f);
        }

    }

    public void PlayAttack(object sender, EventArgs e)
    {
        SFX.volume = 0.5f;
        SFX.pitch = 1.2f;
        SFX.PlayOneShot(Attack);
    }


}
