using UnityEngine;
using TMPro;

public class LivesUI : MonoBehaviour, ISimpleListener
{
    public TextMeshProUGUI livesText;

    void Start()
    {
        SimpleEventBus.Instance.AddListener(GameEventType.LivesChanged, this);
    }

    void OnDestroy()
    {
        if (SimpleEventBus.Instance != null)
        {
            SimpleEventBus.Instance.RemoveListener(GameEventType.LivesChanged, this);
        }
    }

    public void OnEvent(GameEventType eventType, object sender, object param = null)
    {
        if (eventType == GameEventType.LivesChanged)
        {
            int lives = (int)param;
            livesText.text = "Lives: " + lives;
        }
    }
}