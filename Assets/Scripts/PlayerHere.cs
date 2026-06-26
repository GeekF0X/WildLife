using UnityEngine;
using UnityEngine.Events;

public class PlayerHere : MonoBehaviour
{
    GameObject playerHere;
    public GameObject small, big;
    public UnityEvent Event;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == small || other.gameObject == big)
        {
            if (playerHere && other.gameObject != playerHere)
            {
                Event.Invoke();
            }
            else
                playerHere = other.gameObject;
        }
    }

}
