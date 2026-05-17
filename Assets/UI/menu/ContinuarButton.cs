using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinuarButton : MonoBehaviour
{
    public void Continuar()
    {
        SceneManager.LoadScene("Blocagem");
    }
}
