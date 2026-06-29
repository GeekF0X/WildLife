using System;
using Unity.VectorGraphics;
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

public class uiBase : MonoBehaviour
{
    public GameObject configuracao, principal, play, pause;

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
        UnityEngine.SceneManagement.SceneManager.LoadScene("Controles");
    }

    public void Continue()
    {
        Global.LoadCheckpoint();
    }
    public void config()
    {
        principal.SetActive(false);
        configuracao.SetActive(true);
    }
    
    public void voltar()
    {
        principal.SetActive(true);
        configuracao.SetActive(false);
    }

    public void voltardopause()
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
    
    public void playpause()
    {
        pause.SetActive(true);
        principal.SetActive(false);
    }

    public void sair()
    {
        Application.Quit();
    }
}
