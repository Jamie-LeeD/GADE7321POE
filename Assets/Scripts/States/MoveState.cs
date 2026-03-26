using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class MoveState : PlayerState
{
    public MoveState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Update()
    {
        Vector3 inputDir = stateMachine.GetCameraRelativeInput();

        if (inputDir.magnitude < 0.1f)
        {
            stateMachine.moveDirection = Vector3.zero;
            stateMachine.ChangeState(new IdleState(stateMachine));
            return;
        }

        if (!stateMachine.isGrounded)
        {
            stateMachine.ChangeState(new FallState(stateMachine));
            return;
        }

        if (stateMachine.input.SprintHeld)
        {
            stateMachine.ChangeState(new SprintState(stateMachine));
            return;
        }

        stateMachine.moveDirection = inputDir * stateMachine.walkSpeed;

        stateMachine.animator.SetFloat("Speed", stateMachine.moveDirection.magnitude);

        Rotate(inputDir);

        if (stateMachine.input.JumpPressed)
        {
            stateMachine.ChangeState(new JumpState(stateMachine));
        }
    }

    void Rotate(Vector3 dir)
    {
        Quaternion targetRot = Quaternion.LookRotation(dir);
        stateMachine.transform.rotation = Quaternion.Lerp(
            stateMachine.transform.rotation,
            targetRot,
            stateMachine.rotationSpeed * Time.deltaTime
        );
    }
}