using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance { get; private set; }

    [Header("Painéis")]
    [Tooltip("Painel principal de pause")]
    public GameObject pausePanel;
    [Tooltip("Sub-painel de controles (opcional)")]
    public GameObject controlesPanel;
    [Tooltip("Sub-painel de opções/volume (opcional)")]
    public GameObject opcoesPanel;

    [Header("Configurações")]
    [Tooltip("Tecla para abrir/fechar o pause")]
    public KeyCode pauseKey = KeyCode.Escape;
    [Tooltip("Se true, ignora o pause durante a tela de Game Over")]
    public bool blockOnGameOver = true;

    public bool IsPaused { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        if (pausePanel != null) pausePanel.SetActive(false);
        if (controlesPanel != null) controlesPanel.SetActive(false);
        if (opcoesPanel != null) opcoesPanel.SetActive(false);

        IsPaused = false;
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        Debug.Log("ESC apertado!");
        if (blockOnGameOver && GameManager.Instance != null && GameManager.Instance.IsGameOver)
            return;

        if (Input.GetKeyDown(pauseKey))
        {
            if (IsPaused) Resume();
            else Pause();
        }
    }

    public void Pause()
    {
        IsPaused = true;
        if (pausePanel != null) pausePanel.SetActive(true);
        Time.timeScale = 0f;

        if (MouseController.Instance != null)
            MouseController.Instance.UnlockMouse();
    }

    public void Resume()
    {
        IsPaused = false;
        if (pausePanel != null) pausePanel.SetActive(false);
        if (controlesPanel != null) controlesPanel.SetActive(false);
        if (opcoesPanel != null) opcoesPanel.SetActive(false);
        Time.timeScale = 1f;

        if (MouseController.Instance != null)
            MouseController.Instance.LockMouse();

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayUIClick();
    }

    public void OpenControles()
    {
        if (pausePanel != null) pausePanel.SetActive(false);
        if (controlesPanel != null) controlesPanel.SetActive(true);

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayUIClick();
    }

    public void OpenOpcoes()
    {
        if (pausePanel != null) pausePanel.SetActive(false);
        if (opcoesPanel != null) opcoesPanel.SetActive(true);

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayUIClick();
    }

    public void BackToPause()
    {
        if (controlesPanel != null) controlesPanel.SetActive(false);
        if (opcoesPanel != null) opcoesPanel.SetActive(false);
        if (pausePanel != null) pausePanel.SetActive(true);

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayUIBack();
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        if (MouseController.Instance != null)
            MouseController.Instance.LockMouse();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        if (MouseController.Instance != null)
            MouseController.Instance.UnlockMouse(); // Mouse liberado no menu
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}