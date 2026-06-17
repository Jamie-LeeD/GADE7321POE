using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuUI : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    public GameObject gameOverPanel;

    private bool isPaused;

    void Start()
    {
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false);
        }
    }

    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape) || !CanUsePauseMenu())
        {
            return;
        }

        TogglePause();
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Pause()
    {
        isPaused = true;

        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(true);
        }

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        isPaused = false;

        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false);
        }

        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void GoHome()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    private bool CanUsePauseMenu()
    {
        return gameOverPanel == null || !gameOverPanel.activeSelf;
    }
}
