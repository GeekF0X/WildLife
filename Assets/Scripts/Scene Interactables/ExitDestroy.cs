using System;
using UnityEngine;

public class ExitDestroy : MonoBehaviour
{
#nullable enable
    public GameObject? exitObj;
    Collider? coll;

    void Start()
    {
        if (TryGetComponent<Collider>(out coll))
        {
            throw new Exception(gameObject.name + " sem colisor!");
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (coll)
        {
            if (exitObj)
            {
                if (collider.gameObject == exitObj)
                {
                    coll.enabled = false;
                }
            }
            else
            {
                coll.enabled = false;
            }
        }
    }
}
