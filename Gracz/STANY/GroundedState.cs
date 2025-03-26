using UnityEngine;

public class GroundedState : State
{
    MovementManager movementManager;

    float jogLayerWeight;
    float moveSpeed;
    Vector2 moveInput;

    public GroundedState(FiniteStateMachine context, StateFactory factory) : base(context, factory)
    {
        isRootState = true;
        OnSubStateInitialize();
    }

    public override void OnEnter()
    {
        movementManager = manager.GetManager<MovementManager>();
    }

    public override void OnUpdate()
    {
        if(!movementManager.canPerformSpecialMove)
        {
            movementManager.SetMovementSpeed(0f);
            movementManager.SetMovementAnimation(Vector2.zero, 0f, true);
        }

        OnSwitchCheck();
    }

    public override void OnExit()
    {
        
    }

    public override void OnSwitchCheck()
    {
        if(controller.isGrounded && inputHandler.IsJumping && movementManager.canPerformSpecialMove)
            SwitchState(factory.Jump());
        else if(!controller.isGrounded)
            SwitchState(factory.Fall());
    }

    public override void OnSubStateInitialize()
    {
        if(inputHandler.IsDashing && movementManager.canPerformSpecialMove)
            SetSubState(factory.Dash());
        if(inputHandler.MoveInput != Vector2.zero && !inputHandler.IsRunning)
            SetSubState(factory.Walk());
        else if(inputHandler.MoveInput != Vector2.zero)
            SetSubState(factory.Run());
        else
            SetSubState(factory.Idle());
    }
}