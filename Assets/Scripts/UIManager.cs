using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ResumeGame()
    {
        if (GameManager.Instance.GameState == GameState.Paused)
            GameManager.Instance.GameState = GameState.Running;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
