using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public void OnRestartButton()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.RestartLevel();
    }

    public void OnQuitButton()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.QuitToMenu();
    }
}