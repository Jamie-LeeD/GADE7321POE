using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    public Transform player;

    void Start()
    {
        PlayerStats stats = player.GetComponent<PlayerStats>();

        CheckpointManager.Instance.SaveInitialCheckpoint(player, stats);
    }
}