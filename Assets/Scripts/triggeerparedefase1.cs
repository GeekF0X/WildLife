using UnityEngine;

public class triggeerparedefase1 : MonoBehaviour
{
    public paredefase1 p;
    private void OnDestroy()
    {
        p.destruir();
    }
}
