using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class SprintState : PlayerState
{
    public SprintState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        SimpleEventBus.Instance.PostNotification(GameEventType.SprintStart, stateMachine);
        stateMachine.animator.SetFloat("Speed", 2f);
    }

    public override void Exit()
    {
        SimpleEventBus.Instance.PostNotification(GameEventType.SprintStop, stateMachine);
    }

    public override void Update()
    {
        Vector3 inputDir = stateMachine.GetCameraRelativeInput();

        if (inputDir.magnitude < 0.1f || !stateMachine.input.SprintHeld)
        {
            stateMachine.ChangeState(new MoveState(stateMachine));
            return;
        }

        if (!stateMachine.isGrounded)
        {
            stateMachine.ChangeState(new FallState(stateMachine));
            return;
        }

        stateMachine.moveDirection = inputDir * stateMachine.sprintSpeed;
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