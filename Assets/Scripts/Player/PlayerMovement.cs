using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    float velocity;
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        velocity = PlayerGlobals.Instance.Speed * 200;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        float movX = Input.GetAxisRaw("Horizontal");
        float movY = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(movX, movY).normalized * velocity * Time.deltaTime;

    }
}
