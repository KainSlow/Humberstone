using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDelete : MonoBehaviour
{

    [SerializeField] float lifeTime;
    void Start()
    {
        StartCoroutine(Death());
    }

    public IEnumerator Death()
    {

        yield return new WaitForSeconds(lifeTime);

        Destroy(gameObject);

    }
}
