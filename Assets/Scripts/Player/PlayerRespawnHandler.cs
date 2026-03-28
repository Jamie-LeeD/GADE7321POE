using UnityEngine;

public class PlayerRespawnHandler : MonoBehaviour, ISimpleListener
{
    private PlayerStats stats;

    void Start()
    {
        SimpleEventBus.Instance.AddListener(GameEventType.RequestRespawn, this);
        stats = GetComponent<PlayerStats>();
    }

    void OnDisable()
    {
        SimpleEventBus.Instance.RemoveListener(GameEventType.RequestRespawn, this);
    }

    public void OnEvent(GameEventType eventType, object sender, object param = null)
    {
        if (eventType == GameEventType.RequestRespawn)
        {
            RespawnLatest();
        }
        else if (eventType == GameEventType.RestartGame)
        {
            RestartFromBeginning();
        }
    }

    void Respawn()
    {
        CheckpointData checkpoint = CheckpointManager.Instance.GetCurrentCheckpoint();

        if (checkpoint == null)
        {
            Debug.LogError("No checkpoint!");
            return;
        }

        var stateMachine = GetComponent<PlayerStateMachine>();
        var controller = GetComponent<CharacterController>();

        controller.enabled = false;

        transform.position = checkpoint.position + Vector3.up * 2f;

        stateMachine.velocity = Vector3.zero;
        stateMachine.isGrounded = true;

        controller.enabled = true;

        stateMachine.ChangeState(new IdleState(stateMachine));
    }
    void RespawnLatest()
    {
        CheckpointData checkpoint = CheckpointManager.Instance.GetCurrentCheckpoint();

        if (checkpoint == null)
        {
            Debug.LogError("No checkpoint!");
            return;
        }

        Teleport(checkpoint);
    }

    void RestartFromBeginning()
    {
        CheckpointData checkpoint = CheckpointManager.Instance.GetFirstCheckpoint();

        if (checkpoint == null)
        {
            Debug.LogError("No starting checkpoint!");
            return;
        }

        
        var stats = GetComponent<PlayerStats>();
        stats.lives = 3;

        Debug.Log("Restarting from beginning");

        Teleport(checkpoint);
    }
    void Teleport(CheckpointData checkpoint)
    {
        var stateMachine = GetComponent<PlayerStateMachine>();
        var controller = GetComponent<CharacterController>();

        controller.enabled = false;

        transform.position = checkpoint.position + Vector3.up * 2f;

        stateMachine.velocity = Vector3.zero;
        stateMachine.isGrounded = true;

        controller.enabled = true;

        stateMachine.ChangeState(new IdleState(stateMachine));
    }
    void OnDestroy()
    {
        if (SimpleEventBus.Instance != null)
        {
            SimpleEventBus.Instance.RemoveListener(GameEventType.RequestRespawn, this);
        }
    }
}