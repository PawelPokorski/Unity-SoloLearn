using UnityEngine;

public class IdleState : State
{
    MovementManager movementManager;
    
    public IdleState(FiniteStateMachine context, StateFactory factory) : base(context, factory)
    {
        movementManager = manager.GetManager<MovementManager>();
    }

    public override void OnEnter()
    {
    }

    public override void OnUpdate()
    {
        OnSwitchCheck();
        animator.SetBool(MovementManager.IsMovingHash, false);
        movementManager.SetMovementAnimation(Vector2.zero, 0f, true);
        movementManager.SetMovementSpeed(0f, true);
    }

    public override void OnExit()
    {
    }

    public override void OnSwitchCheck()
    {
        if(!movementManager.canPerformSpecialMove) return;
        else if(inputHandler.IsDashing)
            SwitchState(factory.Dash());
        else if(animator.GetBool(MovementManager.IsMovingHash) && inputHandler.IsRunning)
            SwitchState(factory.Run());
        else if(animator.GetBool(MovementManager.IsMovingHash))
            SwitchState(factory.Walk());
    }

    public override void OnSubStateInitialize() {}
}