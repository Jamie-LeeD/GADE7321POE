using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private MyStack<CheckpointData> checkpointStack = new MyStack<CheckpointData>();

    public static CheckpointManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveCheckpoint(Vector3 position, int lives, int score)
    {
        CheckpointData checkpoint =
            new CheckpointData(position, lives, score);

        checkpointStack.Push(checkpoint);

        Debug.Log("Checkpoint Saved");
    }
    public CheckpointData LoadCheckpoint()
    {
        if (checkpointStack.IsEmpty())
        {
            Debug.LogWarning("No checkpoint available!");
            return null;
        }

        return checkpointStack.Peek();
    }
    public CheckpointData GetCurrentCheckpoint() 
    {
        if (checkpointStack.IsEmpty()) 
            return null;
        return checkpointStack.Peek(); 
    }

    public void SaveInitialCheckpoint(Transform player, PlayerStats stats)
    {
        checkpointStack = new MyStack<CheckpointData>(); // clear old data

        CheckpointData checkpoint = new CheckpointData(
            player.position,
            stats.lives,
            stats.score
        );

        checkpointStack.Push(checkpoint);

        Debug.Log("Initial checkpoint saved");
    }

    public CheckpointData GetFirstCheckpoint()
    {
        if (checkpointStack.IsEmpty())
            return null;

        return checkpointStack.GetBottom();
    }
}
