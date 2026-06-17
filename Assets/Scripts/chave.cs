using UnityEngine;

public class chave : MonoBehaviour
{
    public paredefase1 paredefase1;
    private void OnDestroy()
    {
        paredefase1.chavepegar();
    }
}
