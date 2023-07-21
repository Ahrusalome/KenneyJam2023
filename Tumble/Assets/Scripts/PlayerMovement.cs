using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 moveVec;

    private bool isWallSliding;
    private bool isFacingRight;
    private bool isWallJumping = false;

    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(8f, 16f);

    [Header("Player Setup")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private float wallCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundlayer;
    [SerializeField] private LayerMask walllayer;

    [Header("Player properties")]
    [SerializeField] private float speed = 7;
    [SerializeField] private float jumpHeight = 10;
    [SerializeField] private float wallSlidingSpeed = 2f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(isFacingRight && moveVec.x > 0f || !isFacingRight && moveVec.x < 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
        WallSlide();
    }

    private void FixedUpdate()
    {
        if (IsGrounded()) {
            rb.velocity = new Vector2(moveVec.x * speed, rb.velocity.y);
        }
    }

    public void OnMove(InputValue value) => moveVec = value.Get<Vector2>();

    public void OnJump()
    {
        if(IsGrounded()) {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }
        else if (IsWalled())
        {
            isWallJumping = true;
            wallJumpingDirection = -transform.localScale.x;
            rb.AddForce(new Vector2(wallJumpingDirection*1.5f*speed, jumpHeight/1.5f), ForceMode2D.Impulse);

            // if (transform.localScale.x != wallJumpingDirection)
            // {
            //     isFacingRight = !isFacingRight;
            //     Vector3 localScale = transform.localScale;
            //     localScale.x *= -1f;
            //     transform.localScale = localScale;
            // }
            Invoke(nameof(StopWallJumping), wallJumpingDuration);

        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundlayer);
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, groundlayer);
    }

    private void WallSlide()
    {
        if(IsWalled() && !IsGrounded() && moveVec.x != 0 && !isWallJumping)
        {
            // Debug.Log("WallSlide bby");
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
            isWallSliding = false;
    }
}
