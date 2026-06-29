using System;
using Unity.VectorGraphics;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class uiBase : MonoBehaviour
{
    public GameObject configuracao, principal, play, pause;

    private void Start()
    {
        LiberarMouseNoMenu();
    }

    private void LiberarMouseNoMenu()
    {
        Debug.Log("[MenuManager] Liberando mouse...");

        if (MouseController.Instance != null)
        {
            MouseController.Instance.UnlockMouse();
            Debug.Log("[MenuManager] MouseController encontrado. Mouse liberado.");
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Debug.Log("[MenuManager] MouseController não encontrado. Liberado manualmente.");
        }

        Time.timeScale = 1f;
    }

    public void Creditos()
    {
        StartCoroutine(SwitchSceneRoutine());
    }

    private IEnumerator SwitchSceneRoutine()
    {
        SceneManager.LoadScene("Creditos", LoadSceneMode.Additive);
        yield return null;

        var newScene = SceneManager.GetSceneByName("Creditos");
        SceneManager.SetActiveScene(newScene);
    }

    public void Play()
    {
        if (MouseController.Instance != null)
            MouseController.Instance.LockMouse();

        SceneManager.LoadScene("Controles");
    }

    public void Continue()
    {
        if (MouseController.Instance != null)
            MouseController.Instance.LockMouse();

        Global.LoadCheckpoint();
    }

    public void Config()
    {
        principal.SetActive(false);
        configuracao.SetActive(true);
    }

    public void Voltar()
    {
        principal.SetActive(true);
        configuracao.SetActive(false);
    }

    public void VoltarDoPause()
    {
        principal.SetActive(true);
        configuracao.SetActive(false);
    }

    public void Pause()
    {
        pause.SetActive(false);
        play.SetActive(true);
        principal.SetActive(true);
    }

    public void PlayPause()
    {
        pause.SetActive(true);
        principal.SetActive(false);
    }

    public void Sair()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}