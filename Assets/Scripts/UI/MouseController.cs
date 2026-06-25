using UnityEngine;

public class MouseController : MonoBehaviour
{
    public static MouseController Instance { get; private set; }

    [Header("Configurações")]
    [Tooltip("Se true, trava o mouse automaticamente ao iniciar o jogo")]
    public bool lockOnStart = true;

    [Tooltip("Se true, libera o mouse quando a janela perde foco (alt+tab)")]
    public bool unlockOnFocusLost = true;

    public bool IsLocked { get; private set; }

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
        if (lockOnStart)
            LockMouse();
        else
            UnlockMouse();
    }

    public void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        IsLocked = true;
    }

    public void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        IsLocked = false;
    }
    public void ToggleMouse()
    {
        if (IsLocked) UnlockMouse();
        else LockMouse();
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!unlockOnFocusLost) return;
        if (!hasFocus)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (IsLocked && (PauseMenu.Instance == null || !PauseMenu.Instance.IsPaused))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}