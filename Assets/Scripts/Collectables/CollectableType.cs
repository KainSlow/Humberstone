using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableType : MonoBehaviour
{
    [SerializeField] int Type;
    [SerializeField] GameObject itemPickUpSound;
    public int GetObjType()
    {
        return Type;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(itemPickUpSound);
            PlayerGlobals.Instance.SetObjCollected(Type);
            Destroy(gameObject);
        }
    }

}
