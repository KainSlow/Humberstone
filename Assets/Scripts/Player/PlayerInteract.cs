using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] float interactRange;
    [SerializeField] LayerMask NPCLayer;

    private NPCInteractable lastNPC;
    bool isInteracting;

    private void Update()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, interactRange, NPCLayer.value);

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (col != null)
            {
                if (col.transform.parent != null)
                {
                    NPCInteractable npcInt = col.GetComponentInParent<NPCInteractable>();
                    isInteracting = true;
                    npcInt.Interact();

                    if(npcInt.transform.position.x > transform.position.x)
                    {
                        npcInt.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    else
                    {
                        npcInt.GetComponent<SpriteRenderer>().flipX = false;
                    }


                    lastNPC = npcInt;

                    GameObject.Find("CameraHolder").GetComponent<CameraMov>().enabled = false;

                }
            }
        }


        if(col == null && isInteracting)
        {
            GameObject.Find("CameraHolder").GetComponent<CameraMov>().enabled = true;
            lastNPC.DeActivate();
        }

    }
}
