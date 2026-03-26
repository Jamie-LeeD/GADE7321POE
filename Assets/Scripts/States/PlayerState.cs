using UnityEngine;

public abstract class PlayerState
{
    protected PlayerStateMachine stateMachine;

    public PlayerState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void HandleInput() { }
    public virtual void Update() { }
}