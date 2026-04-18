using System;
using Unity.VisualScripting;
using UnityEngine;

public class uiBase : MonoBehaviour
{
    public GameObject configuraçao, principal, play, pause;
    public void Play()
    {

    }
    public void config()
    {
        principal.SetActive(false);
        configuraçao.SetActive(true);
    }
    public void voltar()
    {
        principal.SetActive(true);
        configuraçao.SetActive(false);
    }
    public void voltardopause()
    {
        principal.SetActive(true);
        configuraçao.SetActive(false);
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
