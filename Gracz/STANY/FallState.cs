using UnityEngine;

public class FallState : State
{
    MovementManager movementManager;

    public FallState(FiniteStateMachine context, StateFactory factory) : base(context, factory)
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
        OnSwitchCheck();
    }

    public override void OnExit()
    {
        movementManager.SetMovementSpeed(0f);
        movementManager.SetMovementAnimation(Vector2.zero, 0f, true);
        movementManager.SetMovementActionDone(MovementActiontype.Jump);
    }

    public override void OnSwitchCheck()
    {
        if(controller.isGrounded)
            SwitchState(factory.Grounded());
    }

    public override void OnSubStateInitialize() {}
}