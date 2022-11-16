using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInputManager))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float sprintSpeedMultiplier = 1.5f;
    public float movementMultiplier = 10f;
    
    // movement state variables
    float horizontalMovement;
    float verticalMovement;
    RaycastHit groundHit; // the object beneath the player
    float maxSlopeAngle = 45f;
    bool onSlope;
    bool sprintPressed;

    [Header("Drag")]
    public float rbDrag = 6f;   

    [Header("Jumping")]
    public float jumpHeight = 10f;
    public float timeToJumpApex = 0.5f;
    [SerializeField] float gravity;
    [SerializeField] float jumpForce;
    [SerializeField] float fallMultiplier = 1.5f;
    [SerializeField] float jumpVelocityFalloff = 0.2f;

    // jumping variables
    bool jumpPressed;
    public bool isGrounded;
    bool isFalling;
    bool isJumping;
    public bool canJump;
    public float lastGroundedTime;
    public float lastJumpTime;

    float playerHeight = 2f;

    [Header("Input")]
    [SerializeField] PlayerInputManager _inputs;
    Vector3 moveDirection;
    Rigidbody rb;
    [SerializeField] bool usePhysicsMovement = true;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        // initially calculate; not strictly necessary
        gravity = -2 * (jumpHeight / (Mathf.Pow(timeToJumpApex, 2)));
        jumpForce = 2 * jumpHeight / timeToJumpApex;
    }

    void ControlDrag()
    {
        rb.drag = rbDrag;

    }

    // process inputs and handle all non-movement logic
    private void Update()
    {
        GetInput();
        CheckGround();
        ControlDrag();

        // didactic
        if (!usePhysicsMovement){
            MovePlayerWithTranslation();
        }
    }

    private void CheckGround()
    {
        isGrounded = Physics.SphereCast(transform.position, 0.4f, Vector3.down, out groundHit, playerHeight / 2 + 0.1f);

        if (groundHit.normal != Vector3.up)
        {
            onSlope = true;
            HandleSlope();
        }
        else onSlope = false;
    }

    private void HandleSlope()
    {
        // check that slope is not too steep
        if (Vector3.Angle(Vector3.up, groundHit.normal) < maxSlopeAngle)
        {
            moveDirection = Vector3.ProjectOnPlane(moveDirection, groundHit.normal);
        }
        else
        {
            // allow for any movement that isn't forward 
            if (Vector3.Dot(groundHit.normal, moveDirection) < 0){
                moveDirection = new Vector3(0,0,0);
            }
            isGrounded = false; // don't allow jumping up the slope, skyrim-style
        }

    }

    private void HandleJumpAndGravity()
    {
        // these calculations are here while I tune my parameters
        gravity = -2 * (jumpHeight / (Mathf.Pow(timeToJumpApex, 2)));
        jumpForce = 2 * jumpHeight / timeToJumpApex;

        // 1. Am I on the ground? If not, am I falling? (That is, is jump pressed still and I'm still accelerating upward (my velocity isn't zero in the y))
        // Also, if I pull off a coyote time jump, I want to use the normal gravity during that arc, not the fall gravity!
        if (isGrounded)
        {
            if (jumpPressed) rb.AddForce(jumpForce * transform.up, ForceMode.VelocityChange);
            // rb.AddForce(-0.1f * Vector3.up, ForceMode.Acceleration);
        }
        else
        {
            if (rb.velocity.y < jumpVelocityFalloff)
            {
                rb.AddForce(fallMultiplier * gravity * Vector3.up, ForceMode.Acceleration);
            }
            else
            {
                rb.AddForce(gravity * Vector3.up, ForceMode.Acceleration);
            }
        }

    }

    void Jump()
    {
        Vector3 jumpDir = jumpForce * transform.up;
        
    }

    private void GetInput()
    {
        // update our movement variables    
        GetHorizontalInput();
        GetVerticalInput();
        GetJumpInput();
        GetSprintInput();

        // only apply sprinting to our forward direction
        Vector3 forwardDir = (sprintPressed) ? transform.forward * sprintSpeedMultiplier : transform.forward;
        moveDirection = forwardDir * verticalMovement + transform.right * horizontalMovement;
    }
    private void FixedUpdate()
    {
        if (usePhysicsMovement){
            MovePlayer();
        }
        HandleJumpAndGravity();
    }

    private void MovePlayerWithTranslation()
    {
        Vector3 directionToMove = movementMultiplier * moveSpeed * moveDirection;
        transform.Translate(directionToMove * Time.deltaTime * 0.5f, Space.World);
    }

    private void MovePlayer()
    {
        Vector3 directionToMove = movementMultiplier * moveSpeed * moveDirection;
        rb.AddForce(directionToMove, ForceMode.Acceleration);
    }

    #region Update Movement Variables from Outside Class
    private void GetVerticalInput()
    {
        verticalMovement = _inputs.MovementInput.y;
    }

    private void GetHorizontalInput()
    {
        horizontalMovement = _inputs.MovementInput.x;
    }

    private void GetJumpInput()
    {
        jumpPressed = _inputs.JumpInput;
    }

    private void GetSprintInput()
    {
        sprintPressed = _inputs.SprintInput;
    }
    #endregion
}
