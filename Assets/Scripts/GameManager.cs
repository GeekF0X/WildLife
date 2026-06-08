using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI")]
    public GameObject gameOverPanel;

    [Header("Fail Reason Texts (opcional)")]
    public GameObject waterFailText;
    public GameObject fallFailText;

    public bool IsGameOver { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (waterFailText != null) waterFailText.SetActive(false);
        if (fallFailText != null) fallFailText.SetActive(false);
    }

    public enum FailReason { Water, Fall }

    public void TriggerGameOver(FailReason reason)
    {
        if (IsGameOver) return;
        IsGameOver = true;

        Debug.Log($"[GameManager] GAME OVER — motivo: {reason}");

        if (gameOverPanel != null) gameOverPanel.SetActive(true);

        if (reason == FailReason.Water && waterFailText != null)
            waterFailText.SetActive(true);
        else if (reason == FailReason.Fall && fallFailText != null)
            fallFailText.SetActive(true);

        Time.timeScale = 0f;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}