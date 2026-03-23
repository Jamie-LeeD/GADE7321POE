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
    public CheckpointData GetCurrentCheckpoint()
    {
        if (checkpointStack.IsEmpty())
            return null;

        return checkpointStack.Peek();
    }
}
