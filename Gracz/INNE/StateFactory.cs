public class StateFactory
{
    FiniteStateMachine context;

    public StateFactory(FiniteStateMachine context)
    {
        this.context = context;
    }

    // Substates
    public State Idle() => new IdleState(context, this);
    public State Walk() => new WalkState(context, this);
    public State Run() => new RunState(context, this);
    public State Dash() => new DashState(context, this);

    // Superstates
    public State Grounded() => new GroundedState(context, this);
    public State Jump() => new JumpState(context, this);
    public State Fall() => new FallState(context, this);
}