using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObserver : MonoBehaviour
{
    PlayerManager pM;

    PlayerAnimC pAnim;


    private void Awake()
    {
        pM = GetComponent<PlayerManager>();
        pAnim = GetComponent<PlayerAnimC>();
    }

    private void OnEnable()
    {
        //pM.OnClick += StopPlayer;

        pM.OnClick += StopAim;

        pM.OnClick += PlayAttack;

        pM.OnHit += ApplyKnocback;

        pM.OnHit += PlayHitted;
    }

    private void OnDisable()
    {
        //pM.OnClick -= StopPlayer;

        pM.OnClick -= StopAim;

        pM.OnClick -= PlayAttack;

        pM.OnHit -= ApplyKnocback;

        pM.OnHit -= PlayHitted;

    }
    private void StopPlayer(object sender, EventArgs e) => pM.DisableMov();

    private void StopAim(object sender, EventArgs e) => pM.DisableAim();

    private void ApplyKnocback(object sender, EventArgs e) => pM.DisableMov();

    private void PlayAttack(object sender, EventArgs e) => pAnim.Attack();

    private void PlayHitted(object sender, EventArgs e) => pAnim.Hitted();
   


}
