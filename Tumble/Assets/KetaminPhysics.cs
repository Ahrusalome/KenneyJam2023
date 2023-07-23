using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KetaminPhysics : MonoBehaviour
{
    private PlayerMovement player;
    private Rigidbody2D rb;
    void Start()
    {
        player = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.IsGrounded())
        {
            rb.gravityScale = 0;
        }
        if (!player.IsGrounded() && rb.velocity.y <= 0.15f && rb.gravityScale == 0) {
            rb.velocity = new Vector2(rb.velocity.x, 10f);
        }
    }
}
