using UnityEngine;

public class FallState : PlayerState
{
    private float fallTimer;
    private PlayerStats stats;
    private bool hasTriggeredDeath;

    public FallState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        stats = stateMachine.GetComponent<PlayerStats>();
    }

    public override void Enter()
    {
        fallTimer = 0f;
        hasTriggeredDeath = false;
    }

    public override void Update()
    {
        stateMachine.animator.SetBool("IsGrounded", false);
        fallTimer += Time.deltaTime;

        Vector3 inputDir = stateMachine.GetCameraRelativeInput();

        // Air control
        Vector3 airMove = inputDir * stateMachine.walkSpeed * stateMachine.airControl;
        stateMachine.moveDirection = Vector3.Lerp(
            stateMachine.moveDirection,
            airMove,
            Time.deltaTime
        );


        if (!hasTriggeredDeath && fallTimer >= stateMachine.maxFallTime)
        {
            hasTriggeredDeath = true;

            SimpleEventBus.Instance.PostNotification(GameEventType.PlayerDied, stateMachine);

            return;
        }

        // Landed safely
        if (stateMachine.isGrounded)
        {
            stateMachine.ChangeState(new IdleState(stateMachine));
        }
    }


}