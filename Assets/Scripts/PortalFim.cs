using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalFim : MonoBehaviour
{
    private void OnDestroy()
    {
        SceneManager.LoadScene("blocagem");
    }
}
