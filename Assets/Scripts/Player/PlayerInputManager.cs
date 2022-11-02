using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    PlayerControls playerControls;

    // movement variables
    private Vector2 _movementInput;
    private Vector2 _lookInput;
    private bool _jumpInput;
    private bool _sprintInput;

    public Vector2 MovementInput {get{return _movementInput;}}
    public Vector2 LookInput {get{return _lookInput;}}
    public bool JumpInput {get{return _jumpInput;}}
    public bool SprintInput {get{return _sprintInput;}}

    // interaction and shotting variables
    private bool _interactionInput;
    private bool _shootInput;

    public bool InteractionInput {get{return _interactionInput;}}
    public bool ShootInput {get{return _shootInput;}}

    private void Awake() {
        playerControls = new PlayerControls();

        // connect our movement inputs
        playerControls.Movement.Move.performed += GetMovementInput;
        playerControls.Movement.Move.canceled += GetMovementInput;

        playerControls.Movement.Look.performed += GetLookinput;
        playerControls.Movement.Look.canceled += GetLookinput;

        playerControls.Movement.Jump.performed += GetJumpInput;
        playerControls.Movement.Jump.canceled += GetJumpInput;

        playerControls.Movement.Sprint.performed += GetSprintInput;
        playerControls.Movement.Sprint.canceled += GetSprintInput;

        // connect interaction inputs
        playerControls.Interact.Interact.performed += GetInteractionInput;
        playerControls.Interact.Interact.canceled += GetInteractionInput;

        playerControls.Attack.Fire.performed += GetFireInput;
        playerControls.Attack.Fire.canceled += GetFireInput;
    }

    

    #region Movement Callbacks
    private void GetJumpInput(InputAction.CallbackContext ctx)
    {
        _jumpInput = ctx.ReadValueAsButton();
    }

    private void GetLookinput(InputAction.CallbackContext ctx)
    {
        _lookInput = ctx.ReadValue<Vector2>();
        
    }

    private void GetMovementInput(InputAction.CallbackContext ctx)
    {
        _movementInput = ctx.ReadValue<Vector2>();
    }

    private void GetSprintInput(InputAction.CallbackContext ctx)
    {
        _sprintInput = ctx.ReadValueAsButton();
    }
    #endregion

    #region Interaction Callbacks
    private void GetInteractionInput(InputAction.CallbackContext ctx)
    {
        _interactionInput = ctx.ReadValueAsButton();
    }

    private void GetFireInput(InputAction.CallbackContext ctx){
        _shootInput = ctx.ReadValueAsButton();
    }
    #endregion

    private void OnEnable() {
        playerControls.Enable();
    }

    private void OnDisable() {
        playerControls.Disable();
    }
}
