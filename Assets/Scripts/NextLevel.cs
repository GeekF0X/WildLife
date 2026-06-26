using Unity.VisualScripting;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    bool hasPlayer = false;
    public GameObject small, big;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == small || other.gameObject == big)
        {
            if (hasPlayer)
            {
                Global.LoadScene("Blocagem3");
            }
            else
                hasPlayer = true;
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == small || other.gameObject == big)
        {
            hasPlayer = false;
        }
    }
}
