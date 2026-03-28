using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1); // Level 1
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}