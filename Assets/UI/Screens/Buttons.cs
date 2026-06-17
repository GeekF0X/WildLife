using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void Continuar()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("StoryRobot1");
    }

    public void Jogar()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Blocagem2");
    }

    public void Seguindo()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Blocagem");
    }
}
