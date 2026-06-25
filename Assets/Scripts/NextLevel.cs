using Unity.VisualScripting;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    bool hasPlayer = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
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
        if (other.tag == "Player")
        {
            hasPlayer = false;
        }
    }
}
