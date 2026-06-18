using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinuarButton : MonoBehaviour
{
    public void Continuar()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Blocagem");
    }
}
