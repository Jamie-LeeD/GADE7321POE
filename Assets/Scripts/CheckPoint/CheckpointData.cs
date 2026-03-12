using UnityEngine;

public class CheckpointData
{
    public Vector3 position;
    public int lives;
    public int score;

    public CheckpointData(Vector3 pos, int lives, int score)
    {
        position = pos;
        this.lives = lives;
        this.score = score;
    }
}
