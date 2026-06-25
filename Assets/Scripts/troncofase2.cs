using UnityEngine;

public class troncofase2 : MonoBehaviour
{
    public GameObject partestronco;
    private void OnDestroy()
    {
        partestronco.SetActive(true);
    }
}
