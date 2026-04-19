using UnityEngine;
using UnityEngine.SceneManagement;

public class VoltarButton : MonoBehaviour
{
    public void Voltar()
    {
        SceneManager.LoadScene("testeui");
    }
}
