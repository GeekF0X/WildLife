using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisorSandbox : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "OffLimits")
        {
            SceneManager.LoadScene("Sandbox");
        }
    }
}
