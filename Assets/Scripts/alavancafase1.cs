using UnityEngine;

public class alavancafase1 : MonoBehaviour
{
    public GameObject alavanca, cubo, corpo;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "alavanca")
        {
            corpo.SetActive(true);
            cubo.SetActive(true);
            Destroy(alavanca);
        }
    }
}
