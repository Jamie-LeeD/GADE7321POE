using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour, ISimpleListener
{
    public GameObject gameOverPanel;

    void Start()
    {
        gameOverPanel.SetActive(false);
        SimpleEventBus.Instance.AddListener(GameEventType.GameOver, this);
    }

    void OnDestroy()
    {
        if (SimpleEventBus.Instance != null)
        {
            SimpleEventBus.Instance.RemoveListener(GameEventType.GameOver, this);
        }
    }

    public void OnEvent(GameEventType eventType, object sender, object param = null)
    {
        if (eventType == GameEventType.GameOver)
        {
            gameOverPanel.SetActive(true);

            Time.timeScale = 0f; // pause game

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}