using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    PlayerInputActions inputActions;

    /// <summary>
    /// change [Action] expression from the further command lines to appropriate action name when adding a new action
    /// </summary>

    #region Private Variables

    Vector2 moveInput;
    Vector2 mouseInput;
    bool isRunKeyPressed;
    bool isCrouchKeyPressed;
    bool isInteractKeyPressed;
    bool isMeleeKeyPressed;
    bool isShootKeyPressed;
    bool isReloadKeyPressed;
    bool isAimKeyPressed;
    bool isJumpKeyPressed;
    // Type is[Action]KeyPressed;

    #endregion

    #region Public Fields

    public Vector2 MoveInput => moveInput;
    public Vector2 MouseInput => new (Mathf.Clamp(mouseInput.x, -30f, 30f), mouseInput.y); // Clamp the x-axis to prevent the camera from flipping
    public bool IsMoving => moveInput != Vector2.zero;
    public bool IsRunning => isRunKeyPressed && moveInput.y > 0;
    public bool IsCrouching => isCrouchKeyPressed;
    public bool IsInteracting => isInteractKeyPressed;
    public bool IsPunching => isMeleeKeyPressed;
    public bool IsShooting => isShootKeyPressed;
    public bool IsReloading => isReloadKeyPressed;
    public bool IsAiming => isAimKeyPressed;
    public bool IsJumping => !IsDashing && isJumpKeyPressed;
    public bool IsDashing => IsMoving && moveInput.y <= 0 && isJumpKeyPressed;
    // public Type Is[Action]ing => is[Action]KeyPressed;

    #endregion

    #region Lambda Expressions

    private void SetMoveInput(Vector2 value) => moveInput = value;
    private void SetMouseInput(Vector2 value) => mouseInput = value;
    private void SetRunKey(bool value) => isRunKeyPressed = value;
    private void SetCrouchKey(bool value) => isCrouchKeyPressed = value;
    private void SetInteractKey(bool value) => isInteractKeyPressed = value;
    private void SetMeleeKey(bool value) => isMeleeKeyPressed = value;
    private void SetShootKey(bool value) => isShootKeyPressed = value;
    private void SetReloadKey(bool value) => isReloadKeyPressed = value;
    private void SetAimKey(bool value) => isAimKeyPressed = value;
    private void SetJumpKey(bool value) => isJumpKeyPressed = value;
    // private void Set[Action]Key(Type value) => is[Action]KeyPressed = value;

    #endregion

    #region Finite methods

    private void Start()
    {
        BindAction(inputActions.Locomotion.Movement, SetMoveInput);
        BindAction(inputActions.Locomotion.CameraControl, SetMouseInput);
        BindAction(inputActions.Locomotion.Run, SetRunKey);
        BindAction(inputActions.Locomotion.Crouch, SetCrouchKey);
        BindAction(inputActions.Locomotion.Interact, SetInteractKey);
        BindAction(inputActions.Locomotion.Melee, SetMeleeKey);
        BindAction(inputActions.Locomotion.Shoot, SetShootKey);
        BindAction(inputActions.Locomotion.Reload, SetReloadKey);
        BindAction(inputActions.Locomotion.Aim, SetAimKey);
        BindAction(inputActions.Locomotion.Jump, SetJumpKey);
        // BindAction(inputActions.[ActionMap].[Action], Set[Action]Key);
    }

    private void OnEnable()
    {
        inputActions = new();
        inputActions.Enable();
        SetFocus(true);
    }

    private void OnDisable()
    {
        SetFocus(false);
        inputActions.Disable();
    }

    private void BindAction(InputAction action, Action<bool> setFlag)
    {
        action.performed += _ => setFlag(true);
        action.canceled += _ => setFlag(false);
    }

    private void BindAction(InputAction action, Action<Vector2> setFlag)
    {
        action.performed += _ => setFlag(_.ReadValue<Vector2>());
        action.canceled += _ => setFlag(Vector2.zero);
    }

    private void SetFocus(bool isOnFocus)
    {
        if (isOnFocus)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    #endregion
}