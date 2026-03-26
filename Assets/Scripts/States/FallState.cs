using UnityEngine;

public class FallState : PlayerState
{
    public FallState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Update()
    {
        stateMachine.animator.SetBool("IsGrounded", false);
        Vector3 inputDir = stateMachine.GetCameraRelativeInput();

        Vector3 airMove = inputDir * stateMachine.walkSpeed * stateMachine.airControl;
        stateMachine.moveDirection = Vector3.Lerp(
            stateMachine.moveDirection,
            airMove,
            Time.deltaTime
        );

        if (stateMachine.isGrounded)
        {
            stateMachine.moveDirection = Vector3.zero;
            stateMachine.ChangeState(new IdleState(stateMachine));
        }
    }
}