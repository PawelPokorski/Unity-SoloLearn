using UnityEngine;

public class RunState : State
{
    private MovementManager movementManager;

    public RunState(FiniteStateMachine context, StateFactory factory) : base(context, factory)
    {
        movementManager = manager.GetManager<MovementManager>();
    }

    public override void OnEnter()
    {
    }

    public override void OnUpdate()
    {
        OnSwitchCheck();

        movementManager.SetMovementSpeed(movementManager.runSpeed);
        movementManager.SetMovementAnimation(inputHandler.MoveInput, 1f);
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
        else if(!inputHandler.IsRunning)
            SwitchState(factory.Walk());
    }

    public override void OnSubStateInitialize() {}
}