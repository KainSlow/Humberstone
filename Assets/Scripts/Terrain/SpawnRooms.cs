using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRooms : MonoBehaviour
{

    [SerializeField] LayerMask whatIsRoom;
    [SerializeField] LevelGeneration levelGen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D roomDet = Physics2D.OverlapCircle(transform.position, 1, whatIsRoom);
        if(roomDet == null && levelGen.stopGeneration)
        {
            //Spawn room
            int rand = Random.Range(0, levelGen.rooms.Length);
            Instantiate(levelGen.rooms[rand], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }
}
