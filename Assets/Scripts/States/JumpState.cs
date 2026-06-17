using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class JumpState : PlayerState
{
    public JumpState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        //stateMachine.velocity.y = Mathf.Sqrt(stateMachine.jumpForce * -2f * stateMachine.gravity);
        stateMachine.velocity.y = stateMachine.jumpForce;
        stateMachine.animator.SetBool("IsJumping", true);
        stateMachine.animator.SetTrigger("Jump");

        SimpleEventBus.Instance.PostNotification(GameEventType.Jump, stateMachine);
    }

    public override void Exit()
    {
        stateMachine.animator.SetBool("IsJumping", false);
    }

    public override void Update()
    {
        if (!stateMachine.isGrounded && stateMachine.velocity.y < 0f)
        {
            stateMachine.ChangeState(new FallState(stateMachine));
        }
    }
}