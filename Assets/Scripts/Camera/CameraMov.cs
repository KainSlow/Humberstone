using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMov : MonoBehaviour
{

    [SerializeField] Camera cam;
    [SerializeField] public Transform player;
    [SerializeField] float threshold;

    [SerializeField] Vector3 offSet;
    [SerializeField] float damping;
    private Vector3 velocity = Vector3.zero;


    void FixedUpdate()
    {

        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPos = (player.position + mousePos) / 2f;

        targetPos.x = Mathf.Clamp(targetPos.x, -threshold + player.position.x, threshold + player.position.x);
        targetPos.y = Mathf.Clamp(targetPos.y, -threshold + player.position.y, threshold + player.position.y);
        targetPos.z = -1;

        Vector3 movePos = targetPos + offSet;
        transform.position = Vector3.SmoothDamp(transform.position, movePos, ref velocity, damping);

    }
}
