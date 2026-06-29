using UnityEngine;
using System.Collections.Generic;

public class MouseController : MonoBehaviour
{
    public static MouseController Instance { get; private set; }

    [Header("Configurações")]
    [Tooltip("Se true, trava o mouse automaticamente ao iniciar o jogo")]
    public bool lockOnStart = true;

    [Tooltip("Se true, libera o mouse quando a janela perde foco (alt+tab)")]
    public bool unlockOnFocusLost = true;

    public bool IsLocked { get; private set; }

    private readonly HashSet<string> unlockReasons = new HashSet<string>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
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
        if (unlockReasons.Count > 0) return;

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

    public void RequestUnlock(string reason)
    {
        unlockReasons.Add(reason);
        UnlockMouse();
        Debug.Log($"[MouseController] Mouse liberado por: {reason}. Total razões: {unlockReasons.Count}");
    }


    public void ReleaseUnlock(string reason)
    {
        unlockReasons.Remove(reason);
        Debug.Log($"[MouseController] Razão removida: {reason}. Total restante: {unlockReasons.Count}");

        if (unlockReasons.Count == 0)
            LockMouse();
    }

    public void ForceLockMouse()
    {
        unlockReasons.Clear();
        LockMouse();
    }


    private void OnApplicationFocus(bool hasFocus)
    {
        if (!unlockOnFocusLost) return;

        if (!hasFocus)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (IsLocked && unlockReasons.Count == 0
                 && (PauseMenu.Instance == null || !PauseMenu.Instance.IsPaused))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}