using System;
using UnityEngine;

public class ExitDestroy : MonoBehaviour
{
#nullable enable
    public GameObject? exitObj;
    public Collider[] collisionList;

    private void OnTriggerExit(Collider collider)
    {
        if (collisionList.Length > 0)
        {
            if (exitObj)
            {
                if (collider.gameObject == exitObj)
                {
                    foreach(Collider coll in collisionList)
                        coll.enabled = false;
                }
            }
            else
            {
                foreach (Collider coll in collisionList)
                    coll.enabled = false;
            }
        }
    }
}
