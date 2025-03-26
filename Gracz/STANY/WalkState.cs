using UnityEngine;

public class WalkState : State
{
    private MovementManager movementManager;

    public WalkState(FiniteStateMachine context, StateFactory factory) : base(context, factory)
    {
        movementManager = manager.GetManager<MovementManager>();
    }

    public override void OnEnter()
    {
        Debug.Log("Enter walk");
    }

    public override void OnUpdate()
    {
        OnSwitchCheck();
        Debug.Log("Update walk");
        movementManager.SetMovementSpeed(movementManager.walkSpeed);
        movementManager.SetMovementAnimation(inputHandler.MoveInput, 0f);
    }

    public override void OnExit()
    {
    }

    public override void OnSwitchCheck()
    {
        if(inputHandler.IsDashing && movementManager.canPerformSpecialMove)
            SwitchState(factory.Dash());
        else if(!animator.GetBool(MovementManager.IsMovingHash) || !movementManager.canPerformSpecialMove)
            SwitchState(factory.Idle());
        else if(inputHandler.IsRunning)
            SwitchState(factory.Run());
    }

    public override void OnSubStateInitialize() {}
}