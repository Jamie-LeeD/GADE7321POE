using UnityEngine;

public class PlayerStats : MonoBehaviour, ISimpleListener
{
    public int lives = 3;
    public int score = 0;
    private void Start()
    {
        SimpleEventBus.Instance.AddListener(GameEventType.PlayerDied, this);
        SimpleEventBus.Instance.PostNotification(GameEventType.LivesChanged, this, lives);
        SimpleEventBus.Instance.PostNotification(GameEventType.ScoreChanged, this, score);
    }

    public void OnEvent(GameEventType eventType, object sender, object param = null)
    {
        if (eventType == GameEventType.PlayerDied)
        {
            LoseLife();
        }
    }

    public void AddScore(int amount)
    {
        score += amount;

        Debug.Log("Score: " + score);

        SimpleEventBus.Instance.PostNotification(
            GameEventType.ScoreChanged,
            this,
            score
        );
    }

    public void LoseLife()
    {
        lives--;

        SimpleEventBus.Instance.PostNotification(GameEventType.LivesChanged, this, lives);

        if (lives <= 0)
        {
            Debug.Log("Game Over");

            SimpleEventBus.Instance.PostNotification(GameEventType.GameOver, this);
            return;
        }

        SimpleEventBus.Instance.PostNotification(GameEventType.RequestRespawn, this);
    }

    void OnDestroy()
    {
        if (SimpleEventBus.Instance != null)
        {
            SimpleEventBus.Instance.RemoveListener(GameEventType.PlayerDied, this);
        }
    }
}