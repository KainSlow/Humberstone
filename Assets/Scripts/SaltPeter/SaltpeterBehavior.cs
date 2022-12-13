using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SaltpeterBehavior : MonoBehaviour
{
    #region DropSettings
    [SerializeField] GameObject saltpeterParticles;

    [SerializeField] GameObject cSaltpeter;
    [SerializeField] int dropQuantity;
    [SerializeField] int maxHits;
    #endregion

    #region AnimSettings
    [SerializeField] float animTime;
    [SerializeField] float animSpeed;
    [SerializeField] float animIntervals;

    [SerializeField] float maxMagnitude;
    #endregion

    public EventHandler OnHit;

    Timer hitAnim;
    Timer hitInterval;



    private float animAngle;
    // Start is called before the first frame update
    void Start()
    {
        hitAnim = new Timer(animTime);
        hitInterval = new Timer((float)(animTime / animIntervals));
        hitInterval.ActivateLooping();

        OnHit += DropSaltpeter;
        OnHit += PlayParticles;
        hitAnim.OnTime += Death;
        hitAnim.OnTime += StopInterval;

        hitInterval.OnTime += ChangeDir;
    }

    // Update is called once per frame
    void Update()
    {
        hitAnim.Update();
        hitInterval.Update();

        if (hitAnim.isActive)
        {
            PlayAnim();
        }
        else
        {
            transform.eulerAngles = Vector3.zero;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }


    private void PlayParticles(object sender, EventArgs e)
    {
        Instantiate(saltpeterParticles, transform.position, Quaternion.identity, null);
    }
    private void PlayAnim()
    {
        //Shake
        if (hitInterval.isActive)
        {
            animAngle += animSpeed;
        }
        transform.eulerAngles = new Vector3(0f, 0f, animAngle);


        //Scale
        if(hitAnim.CurrentTime < animTime / 4f)
        {
            if(transform.localScale.magnitude < maxMagnitude)
            {
                transform.localScale *= 1.02f;

            }
        }
        else
        {
            if(transform.localScale.magnitude > 2)
            {
                transform.localScale *= 0.995f;
            }
        }
    }

    public void OnHitted(EventArgs e)
    {
        if (maxHits > 0)
        {
            hitAnim.Start();
            hitInterval.Start();
        }

        EventHandler handler = OnHit;
        handler?.Invoke(this, e);

        animAngle = 0f;
        transform.eulerAngles = Vector3.zero;

    }

    private void DropSaltpeter(object sender, EventArgs e)
    {
        if(maxHits > 0)
        {
            for (int i = 0; i < dropQuantity; i++)
            {
                Instantiate(cSaltpeter, transform.position, Quaternion.identity, null);
            }

            maxHits--;
        }
    }

    private void StopInterval(object sender, EventArgs e)
    {
        hitInterval.Stop();
    }

    private void ChangeDir(object sender, EventArgs e)
    {
        animSpeed = -animSpeed;
    } 

    private void Death(object sender, EventArgs e)
    {

        if (maxHits <= 0)
        {
            Destroy(gameObject);
        }
    }
}
