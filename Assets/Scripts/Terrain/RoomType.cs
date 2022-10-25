using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomType : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private int type;

    public void RoomDestroy()
    {
        Destroy(gameObject);
    }


    public int getType()
    {
        return type;
    }
}
