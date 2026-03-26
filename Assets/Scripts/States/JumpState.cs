using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class JumpState : PlayerState
{
    public JumpState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.velocity.y = stateMachine.jumpForce;
        stateMachine.animator.SetTrigger("Jump");

        SimpleEventBus.Instance.PostNotification(GameEventType.Jump, stateMachine);
    }

    public override void Update()
    {
        if (stateMachine.velocity.y < 0)
        {
            stateMachine.ChangeState(new FallState(stateMachine));
        }
    }
}