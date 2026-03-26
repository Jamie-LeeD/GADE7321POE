using UnityEngine;

public class DialogueLockState : PlayerState
{
    public DialogueLockState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Entered DialogueLockState");
        stateMachine.canMove = false;
        stateMachine.moveDirection = Vector3.zero;
        stateMachine.velocity = Vector3.zero;

        stateMachine.animator.SetFloat("Speed", 0f);
        
        if (stateMachine.cameraController != null)
            stateMachine.cameraController.enabled = false;

        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public override void Exit()
    {
        stateMachine.canMove = true;

        if (stateMachine.cameraController != null)
            stateMachine.cameraController.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}