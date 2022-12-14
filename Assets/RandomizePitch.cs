using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizePitch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float newPitch = Random.Range(0.8f,1.2f);
        GetComponent<AudioSource>().pitch = newPitch;
    }

   
}
