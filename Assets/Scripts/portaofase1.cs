using UnityEngine;

public class portaofase1 : MonoBehaviour
{
    public AudioSource som;

    public void somportao()
    {
        som.Stop();
        som.Play();
    }
}
