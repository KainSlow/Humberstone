using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObserver : MonoBehaviour
{
    PlayerManager pM;

    private void Awake()
    {
        pM = GetComponent<PlayerManager>();
    }

    private void OnEnable()
    {
        pM.OnClick += StopPlayer;

        pM.OnHit += ApplyKnocback;
    }

    private void OnDisable()
    {
        pM.OnClick -= StopPlayer;

        pM.OnHit -= ApplyKnocback;
    }
    private void StopPlayer(object sender, EventArgs e) => pM.DisableMov();


    private void ApplyKnocback(object sender, EventArgs e) => pM.DisableMov();
   


}
