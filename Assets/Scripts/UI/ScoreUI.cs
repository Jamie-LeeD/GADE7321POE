using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour, ISimpleListener
{
    public TextMeshProUGUI scoreText;

    void Start()
    {
        SimpleEventBus.Instance.AddListener(GameEventType.ScoreChanged, this);
    }

    void OnDestroy()
    {
        if (SimpleEventBus.Instance != null)
        {
            SimpleEventBus.Instance.RemoveListener(GameEventType.ScoreChanged, this);
        }
    }

    public void OnEvent(GameEventType eventType, object sender, object param = null)
    {
        if (eventType == GameEventType.ScoreChanged)
        {
            int score = (int)param;
            scoreText.text = "Score: " + score;
        }
    }
}