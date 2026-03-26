using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class IdleState : PlayerState
{
    public IdleState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.moveDirection = Vector3.zero;
        stateMachine.animator.SetFloat("Speed", 0f);
    }
    public override void Update()
    {
        Vector3 inputDir = stateMachine.GetCameraRelativeInput();

        if (!stateMachine.isGrounded)
        {
            stateMachine.ChangeState(new FallState(stateMachine));
            return;
        }

        if (inputDir.magnitude > 0.1f)
        {
            stateMachine.ChangeState(new MoveState(stateMachine));
            return;
        }

        if (stateMachine.input.JumpPressed)
        {
            stateMachine.ChangeState(new JumpState(stateMachine));
        }
    }
}