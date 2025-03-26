using UnityEngine;

public class JumpState : State
{
    MovementManager movementManager;

    public JumpState(FiniteStateMachine context, StateFactory factory) : base(context, factory)
    {
        isRootState = true;
        movementManager = manager.GetManager<MovementManager>();
        OnSubStateInitialize();
    }

    public override void OnEnter()
    {
        MakeJump(context.GeneralManager.GetManager<MovementManager>());
    }

    public override void OnUpdate()
    {
        OnSwitchCheck();
    }

    public override void OnExit()
    {
    }

    public override void OnSwitchCheck()
    {
        if(!controller.isGrounded && controller.velocity.y < 0)
            SwitchState(factory.Fall());
        else if(controller.isGrounded)
            SwitchState(factory.Grounded());
    }

    public override void OnSubStateInitialize()
    {
    }

    private void MakeJump(MovementManager manager)
    {
        controller.Move(Vector3.up * manager.jumpForce * Time.deltaTime);
    }
}