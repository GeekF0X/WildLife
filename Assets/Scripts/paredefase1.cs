using UnityEngine;

public class paredefase1 : MonoBehaviour
{
    bool chave = false;
    public void destruir()
    {
        if (chave)
        {
            Destroy(this.gameObject);
        }
    }
    public void chavepegar()
    {
        chave = true;
    }
}
