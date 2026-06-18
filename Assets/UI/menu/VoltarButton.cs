using UnityEngine;
using UnityEngine.SceneManagement;

public class VoltarButton : MonoBehaviour
{
    public void Voltar()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("testeui");
    }
}
