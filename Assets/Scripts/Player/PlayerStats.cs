using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int lives = 3;
    public int score = 0;

    public void AddScore(int amount)
    {
        score += amount;
    }

    public void LoseLife()
    {
        lives--;

        if (lives <= 0)
        {
            Debug.Log("Game Over");
        }
    }
}