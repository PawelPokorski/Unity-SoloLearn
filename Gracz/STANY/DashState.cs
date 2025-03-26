using System.Collections;
using UnityEngine;

public class DashState : State
{
    public DashState(FiniteStateMachine context, StateFactory factory) : base(context, factory)
    {
    }

    MovementManager movementManager;    

    public override void OnEnter()
    {
        Debug.Log("Dash");
        movementManager = manager.GetManager<MovementManager>();
        movementManager.moveSpeed = movementManager.dashForce;
    }

    public override void OnUpdate()
    {
        movementManager.moveSpeed = Mathf.Lerp(movementManager.moveSpeed, 0f, 10f * Time.deltaTime);
        OnSwitchCheck();
    }

    public override void OnExit()
    {
        movementManager.SetMovementActionDone(MovementActiontype.Dash);
    }

    public override void OnSwitchCheck()
    {
        if(movementManager.moveSpeed < 0.2f)
        {
            movementManager.moveSpeed = 0f;
            SwitchState(factory.Idle());
        }
    }

    public override void OnSubStateInitialize() {}
}