using UnityEngine;

public abstract class State
{
    private State currentSubState;
    private State currentSuperState;
    
    protected bool isRootState = false;
    protected FiniteStateMachine context;
    protected StateFactory factory;
    protected InputHandler inputHandler;
    protected Animator animator;
    protected GeneralManager manager;
    protected CharacterController controller;

    public State(FiniteStateMachine context, StateFactory factory)
    {
        this.context = context;
        this.factory = factory;
        inputHandler = context.InputHandler;
        animator = context.Animator;
        manager = context.GeneralManager;
        controller = context.Controller;
    }

    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();
    public abstract void OnSwitchCheck();
    public abstract void OnSubStateInitialize();

    public void UpdateStates()
    {
        OnUpdate();

        currentSubState?.OnUpdate();
    }

    protected void SwitchState(State newState)
    {
        OnExit();
        newState.OnEnter();

        if(isRootState)
            context.currentState = newState;
        else
            currentSuperState?.SetSubState(newState);
    }

    protected void SetSubState(State subState)
    {
        currentSubState = subState;
        subState.SetSuperState(this);
    }

    protected void SetSuperState(State superState)
    {
        currentSuperState = superState;
    }
}