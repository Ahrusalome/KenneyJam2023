using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rbody;
    private Vector2 movement = Vector2.zero;
    private bool hasWallJumped = false;
    private RaycastHit2D hit2D;
    [SerializeField] private bool isGrounded = false;

    [Header("Player properties")]
    [SerializeField] private Transform slopeRayCastOrigin;
    [SerializeField] private Transform playerFeet;
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpHeight = 5;
    //[SerializeField] private float wallJumpModifier = 1.5f;

    public float Speed
    {
        get { return speed; }   
        set { speed = value; }  
    }
    public float JumpHeight
    {
        get { return jumpHeight; }
        set { jumpHeight = value; }
    }

    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        if (isGrounded) {
            rbody.velocity = new Vector2(movement.x * speed/1.5f, rbody.velocity.y);
        }
        GroundCheck();
    }

    // public void OnJump()
    // {
    //     if (isGrounded)
    //     {
    //         if(hasWallJumped){
    //             rbody.AddForce(new Vector2(-movement.x*1.5f*speed, jumpHeight/1.5f), ForceMode2D.Impulse);
    //         } else {
    //             rbody.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
    //         }
    //         isGrounded = false;
    //     }
    // }

    public void OnMove(InputValue val)
    {
        movement = val.Get<Vector2>();
    }

    void OnCollisionStay2D(Collision2D collision) {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.2f)
            {
                isGrounded = true;
                hasWallJumped = false;
            }
            else {
                if (!hasWallJumped) {
                isGrounded = true;
                hasWallJumped = true;
                }
            }
        }
    }

    void GroundCheck() {
        hit2D = Physics2D.Raycast(slopeRayCastOrigin.position, Vector2.down, 300f, LayerMask.GetMask("Ground"));
        if (hit2D) {
            Vector2 temp = playerFeet.position;
            temp.y = hit2D.point.y;
            playerFeet.position = temp;
        }
    }
}