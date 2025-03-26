using System.Collections;
using UnityEngine;

public class MovementManager : Manager
{
    [Header("Movement speeds")]
    public float walkSpeed;
    public float runSpeed;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;

    [Header("Dashing")]
    public float dashForce;
    public float dashCooldown;

    public bool canPerformSpecialMove { get; set; } // determines if player can jump or dash
    private Coroutine specialMoveCooldown;


    [Header("Movement settings")]
    [SerializeField] float moveSmoothTime;
    public float moveSpeed = 1.5f;
    

    void Start()
    {
        animator.SetBool(HasControlHash, true);
    }

    void FixedUpdate()
    {
        UpdateControlState();
        HandleMovement(inputHandler.MoveInput);
    }

    #region Movement Handling

    // Handle the player movement based on input
    void HandleMovement(Vector2 rawInput)
    {
        if(!controller.enabled) return;

        var smoothInput = new Vector2 (animator.GetFloat(MoveXHash), animator.GetFloat(MoveYHash));

        animator.SetBool(IsMovingHash, rawInput != Vector2.zero || smoothInput.magnitude > 0.25f);

        Vector3 moveDirection = GetMoveDirection(smoothInput);

        controller.SimpleMove(moveDirection * moveSpeed);
    }

    Vector3 GetMoveDirection(Vector2 input)
    {
        Transform camera = Camera.main.transform;

        var cameraRight = new Vector3 (camera.right.x, 0f, camera.right.z).normalized;
        var cameraForward = new Vector3 (camera.forward.x, 0f, camera.forward.z).normalized;

        return cameraRight * input.x + cameraForward * input.y;
    }

    void UpdateControlState()
    {
        controller.enabled = animator.GetBool(HasControlHash);
        canPerformSpecialMove = specialMoveCooldown == null;
    }

    public void SetMovementAnimation(Vector2 input, float jogLayWeight, bool instantly = false)
    {
        if(instantly)
        {
            animator.SetFloat(MoveXHash, input.x);
            animator.SetFloat(MoveYHash, input.y);
            animator.SetLayerWeight(1, jogLayWeight);
        }
        else
        {
            animator.SetFloat(MoveXHash, input.x, moveSmoothTime, moveSpeed * Time.deltaTime * 2f);
            animator.SetFloat(MoveYHash, input.y, moveSmoothTime, moveSpeed * Time.deltaTime * 2f);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), jogLayWeight, Time.deltaTime * 2f));
        }

    }

    public void SetMovementSpeed(float speed, bool instantly = false)
    {
        if(instantly)
            moveSpeed = speed;
        else
            moveSpeed = Mathf.Lerp(moveSpeed, speed, Time.deltaTime * 2f);
    }

    #endregion

    public static int MoveXHash, MoveYHash, HasControlHash, IsMovingHash, AnimatorSpeedHash;

    void OnEnable()
    {
        MoveXHash = Animator.StringToHash("moveX");
        MoveYHash = Animator.StringToHash("moveY");
        HasControlHash = Animator.StringToHash("hasControl");
        IsMovingHash = Animator.StringToHash("isMoving");
        AnimatorSpeedHash = Animator.StringToHash("animatorSpeedMultiplier");
    }

    public void SetMovementActionDone(MovementActiontype type)
    {
        switch(type)
        {
            case MovementActiontype.Dash:
                specialMoveCooldown = StartCoroutine(DashCooldown());
                break;
            case MovementActiontype.Jump:
                specialMoveCooldown = StartCoroutine(JumpCooldown());
                break;
            default:
                break;
        }
    }

    // For jump / dash 
    IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(jumpCooldown);
        specialMoveCooldown = null;
    }

    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);
        specialMoveCooldown = null;
    }
}

public enum MovementActiontype
{
    Dash,
    Jump//,
    // ...
}