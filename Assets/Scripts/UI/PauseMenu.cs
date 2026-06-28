using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance { get; private set; }

    [Header("Painéis Principais")]
    public GameObject pausePanel;
    public GameObject controlesPanel;
    public GameObject opcoesPanel;
    public GameObject creditosPanel;
    public GameObject coletaveisPanel;

    [Header("Sistemas")]
    [Tooltip("Referência para o DataManager (que tem o checkpoint system)")]
    public DataManager dataManager;

    [Header("Cenas")]
    [Tooltip("Índice da cena do menu principal")]
    public int mainMenuSceneIndex = 0;

    [Header("Configurações")]
    public KeyCode pauseKey = KeyCode.Escape;
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
        CloseAllPanels();
        IsPaused = false;
        Time.timeScale = 1f;

        if (dataManager == null)
            dataManager = FindFirstObjectByType<DataManager>();
    }

    private void Update()
    {
        if (blockOnGameOver && GameManager.Instance != null && GameManager.Instance.IsGameOver)
            return;

        if (Input.GetKeyDown(pauseKey))
        {
            if (IsPaused) Resume();
            else Pause();
        }
    }

    private void CloseAllPanels()
    {
        if (pausePanel != null) pausePanel.SetActive(false);
        if (controlesPanel != null) controlesPanel.SetActive(false);
        if (opcoesPanel != null) opcoesPanel.SetActive(false);
        if (creditosPanel != null) creditosPanel.SetActive(false);
        if (coletaveisPanel != null) coletaveisPanel.SetActive(false);
    }


    public void Pause()
    {
        IsPaused = true;
        if (pausePanel != null) pausePanel.SetActive(true);
        Time.timeScale = 0f;

        if (MouseController.Instance != null)
            MouseController.Instance.RequestUnlock("pause");

        Debug.Log("[PauseMenu] PAUSADO");
    }

    public void Resume()
    {
        IsPaused = false;
        CloseAllPanels();
        Time.timeScale = 1f;

        if (MouseController.Instance != null)
            MouseController.Instance.ReleaseUnlock("pause");

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayUIClick();

        Debug.Log("[PauseMenu] Continuando...");
    }


    public void OpenControles() => SwitchPanel(controlesPanel);
    public void OpenOpcoes() => SwitchPanel(opcoesPanel);
    public void OpenCreditos() => SwitchPanel(creditosPanel);
    public void OpenColetaveis() => SwitchPanel(coletaveisPanel);

    private void SwitchPanel(GameObject panelToOpen)
    {
        if (pausePanel != null) pausePanel.SetActive(false);
        if (controlesPanel != null) controlesPanel.SetActive(false);
        if (opcoesPanel != null) opcoesPanel.SetActive(false);
        if (creditosPanel != null) creditosPanel.SetActive(false);
        if (coletaveisPanel != null) coletaveisPanel.SetActive(false);

        if (panelToOpen != null) panelToOpen.SetActive(true);

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayUIClick();
    }

    public void BackToPause()
    {
        CloseAllPanels();
        if (pausePanel != null) pausePanel.SetActive(true);

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayUIBack();
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        if (MouseController.Instance != null)
            MouseController.Instance.ForceLockMouse();

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayUIClick();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("[PauseMenu] Reiniciando fase...");
    }

    public void GoToLastCheckpoint()
    {
        Time.timeScale = 1f;

        if (dataManager == null)
        {
            Debug.LogError("[PauseMenu] DataManager não foi atribuído! Não posso carregar checkpoint.");
            return;
        }

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayUIClick();

        int savedCheckpointIndex = dataManager.checkpointIndex;

        if (savedCheckpointIndex < 0 || savedCheckpointIndex >= dataManager.levelCheckpoints.Count)
        {
            Debug.LogWarning($"[PauseMenu] Checkpoint inválido: índice {savedCheckpointIndex}. Retornando ao início.");
            Resume();
            return;
        }

        Checkpoint lastCheckpoint = dataManager.levelCheckpoints[savedCheckpointIndex];

        if (lastCheckpoint == null)
        {
            Debug.LogWarning("[PauseMenu] Checkpoint é null!");
            Resume();
            return;
        }

        if (dataManager.small != null && lastCheckpoint.smallPosition != null)
        {
            dataManager.small.transform.position = lastCheckpoint.smallPosition.position;
            dataManager.small.transform.rotation = lastCheckpoint.smallPosition.rotation;
            dataManager.small.isEnergized = true;
            Debug.Log($"[PauseMenu] Robô pequeno teleportado para {lastCheckpoint.smallPosition.name}");
        }

        if (dataManager.big != null && lastCheckpoint.bigPosition != null)
        {
            dataManager.big.transform.position = lastCheckpoint.bigPosition.position;
            dataManager.big.transform.rotation = lastCheckpoint.bigPosition.rotation;
            dataManager.big.isEnergized = false;
            Debug.Log($"[PauseMenu] Robô grande teleportado para {lastCheckpoint.bigPosition.name}");
        }

        Debug.Log($"[PauseMenu] Carregado checkpoint #{savedCheckpointIndex}!");

        Resume();
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        if (MouseController.Instance != null)
            MouseController.Instance.UnlockMouse();

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayUIClick();

        SceneManager.LoadScene(mainMenuSceneIndex);
        Debug.Log("[PauseMenu] Indo para Menu Principal...");
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif

        Debug.Log("[PauseMenu] Saindo do jogo...");
    }
}