using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public void OnRestartButton()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.RestartLevel(false);
    }

    public void OnRestartLevelButton()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.RestartLevel(true);
    }

    public void OnQuitButton()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.QuitToMenu();
    }
}