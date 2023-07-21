using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rbody;
    private Vector2 movement = Vector2.zero;
    private bool hasWallJumped = false;
    [SerializeField]ContactFilter2D contactFilter;
    [SerializeField] private bool isGrounded = false;

    [Header("Player properties")]
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpHeight = 5;
    [SerializeField] private float wallJumpModifier = 1.5f;

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

        List<RaycastHit2D> hit = new List<RaycastHit2D>();

        Physics2D.BoxCast(transform.position, Vector2.one, 0f, Vector2.down, contactFilter.NoFilter(), hit);
        foreach (RaycastHit2D item in hit)
        {
            
        }
    }

    public void OnJump()
    {
        if (isGrounded)
        {
            if(hasWallJumped){
                rbody.AddForce(new Vector2(-movement.x*wallJumpModifier*speed, jumpHeight/2), ForceMode2D.Impulse);
            } else {
                rbody.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
            }
            isGrounded = false;
        }
    }

    public void OnMove(InputValue val)
    {
        movement = val.Get<Vector2>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
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
}