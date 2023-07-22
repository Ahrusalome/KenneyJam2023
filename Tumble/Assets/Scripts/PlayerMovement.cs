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
    private bool onImpulse = false;
    private float wallJumpingDirection;
    private float wallJumpingDuration = 0.4f;
    private float airControlSlowDown = 2f;
    private Vector2 wallJumpingPower = new Vector2(8f, 16f);
    private float lastJumpPressed;
    private float timeLeftGround;
    private bool ground;
    private bool hasDefiedEdge;

    [Header("Player Setup")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private float wallCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundlayer;
    [SerializeField] private LayerMask walllayer;
    [SerializeField] private float jumpBuffer = 0.15f;
    [SerializeField] private float coyoteTimeThreshold = 0.2f;

    [Header("Player properties")]
    [SerializeField] private float speed = 7;
    [SerializeField] private float jumpHeight = 10;
    [SerializeField] private float wallSlidingSpeed = 2f;
    [SerializeField] private Vector2 characterBounds;
    [SerializeField] private Vector2 EdgesDetector;
    private bool coyote => !IsGrounded() && timeLeftGround + coyoteTimeThreshold > Time.time;
    private bool bufferedJump => IsGrounded() && lastJumpPressed + jumpBuffer > Time.time;

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
        if (!IsGrounded()) {
            DefyEdges();
        } else {
            hasDefiedEdge = false;
        }
        if (IsGrounded() != ground) {
            timeLeftGround = Time.time;
            ground = IsGrounded();
        }
        if(bufferedJump) {
            OnJump();
        }
    }

    private void FixedUpdate()
    {
        if (IsGrounded()) {
            onImpulse = false;
        }
        if (!onImpulse) {
            if (IsGrounded()) {
                rb.velocity = new Vector2(moveVec.x * speed, rb.velocity.y);
            } else {
                rb.velocity = new Vector2(moveVec.x * speed/airControlSlowDown, rb.velocity.y);
            }
        }
    }

    public void OnMove(InputValue value) => moveVec = value.Get<Vector2>();

    public void OnJump()
    {
        
        if(IsGrounded() || bufferedJump || coyote) {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }
        else if (IsWalled())
        {
            onImpulse = true;
            isWallJumping = true;
            wallJumpingDirection = -transform.localScale.x;
            rb.AddForce(new Vector2(wallJumpingDirection*1.5f*speed, jumpHeight), ForceMode2D.Impulse);

            // if (transform.localScale.x != wallJumpingDirection)
            // {
            //     isFacingRight = !isFacingRight;
            //     Vector3 localScale = transform.localScale;
            //     localScale.x *= -1f;
            //     transform.localScale = localScale;
            // }
            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
        lastJumpPressed = Time.time;
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
        onImpulse = false;
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

    private void DefyEdges() {
        var pos = transform.position;
        var hit = Physics2D.OverlapBox(pos, characterBounds, 0, groundlayer);
        var newPos = new Vector3(0,0);
        if (hit && !hasDefiedEdge) {
            var edgeTopRight = Physics2D.Raycast(pos, EdgesDetector, 0.8f, groundlayer);
            var emptyTopRight = Physics2D.Raycast(new Vector2(pos.x-0.4f, pos.y), EdgesDetector, 0.8f, groundlayer);
            var edgeBotRight = Physics2D.Raycast(pos, new Vector2(EdgesDetector.y, -EdgesDetector.x), 0.8f, groundlayer);
            var emptyBotRight = Physics2D.Raycast(new Vector2(pos.x+0.3f, pos.y-0.4f), EdgesDetector, 0.8f, groundlayer);
            var edgeTopLeft = Physics2D.Raycast(pos, new Vector2(-EdgesDetector.x, EdgesDetector.y), 0.8f, groundlayer);
            var emptyTopLeft = Physics2D.Raycast(new Vector2(pos.x+0.4f, pos.y), new Vector2(-EdgesDetector.x, EdgesDetector.y), 0.8f, groundlayer);
            var edgeBotLeft = Physics2D.Raycast(pos, new Vector2(-EdgesDetector.y, -EdgesDetector.x), 0.8f, groundlayer);
            var emptyBotLeft = Physics2D.Raycast(new Vector2(pos.x-0.3f, pos.y-0.4f), new Vector2(-EdgesDetector.x, EdgesDetector.y), 0.8f, groundlayer);
            if(((edgeTopRight.collider != null && !IsWalled()&& emptyTopLeft.collider == null)||(edgeBotLeft.collider != null && IsWalled()&&emptyBotLeft.collider ==null))) 
            {
                hasDefiedEdge = true;
                newPos = new Vector3(-EdgesDetector.x/2, 0.5f);
                transform.position += newPos;
            } else if (((edgeTopLeft.collider != null && !IsWalled() && emptyTopRight.collider == null)||(edgeBotRight.collider != null && IsWalled() && emptyBotRight.collider == null)))
            {
                hasDefiedEdge = true;
                Debug.DrawRay(pos, new Vector2(EdgesDetector.y, -EdgesDetector.x), Color.red,10f);
                Debug.DrawRay(new Vector2(pos.x+0.3f, pos.y-0.4f), EdgesDetector,Color.green, 10f);
                newPos = new Vector3(EdgesDetector.x/2, 0.5f);
                transform.position += newPos;
            }
        }
    }
}
