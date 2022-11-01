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

                    lastNPC = npcInt;
                }
            }
        }


        if(col == null && isInteracting)
        {
            lastNPC.DeActivate();
        }

    }
}
