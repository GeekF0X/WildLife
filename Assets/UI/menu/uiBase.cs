using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class uiBase : MonoBehaviour
{
    public GameObject configuracao, principal, play, pause;

    public void Sandbox()
    {
        SceneManager.LoadScene("Sandbox");
    }

    public void Play()
    {
        SceneManager.LoadScene("Blocagem");
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
