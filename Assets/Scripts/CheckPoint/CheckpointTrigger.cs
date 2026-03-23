using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats stats = other.GetComponent<PlayerStats>();

            CheckpointManager.Instance.SaveCheckpoint(
                transform.position,
                stats.lives,
                stats.score
            );
        }
    }
}