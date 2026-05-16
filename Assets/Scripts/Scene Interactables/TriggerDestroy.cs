using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerDestroy : MonoBehaviour
{
    public List<GameObject> trackingObjs;
    public Collider[] collisionList;

    public enum Behaviors { Enter, Exit, StayActivate, StayDeactivate}
    public Behaviors behavior;

    UnityAction<Collider> EnterBehavior = (Collider col) => { return; };
    UnityAction<Collider> ExitBehavior = (Collider col) => { return; };

    void Deactivate(Collider collider)
    {
        if (collisionList.Length > 0)
        {
            if (trackingObjs.Count > 0)
            {
                if (trackingObjs.Contains(collider.gameObject))
                {
                    foreach (Collider coll in collisionList)
                        coll.enabled = false;
                    return;
                }
            }
            else
            {
                foreach (Collider coll in collisionList)
                    coll.enabled = false;
            }
        }
    }
    void Activate(Collider collider)
    {
        if (collisionList.Length > 0)
        {
            if (trackingObjs.Count > 0)
            {
                if (trackingObjs.Contains(collider.gameObject))
                {
                    foreach (Collider coll in collisionList)
                        coll.enabled = true;
                    return;
                }
            }
            else
            {
                foreach (Collider coll in collisionList)
                    coll.enabled = true;
            }
        }
    }


    private void Start()
    {
        
        switch (behavior)
        {
            case Behaviors.Enter:
                EnterBehavior = Deactivate;
                break;
            case Behaviors.Exit:
                ExitBehavior = Deactivate;
                break;
            case Behaviors.StayActivate:
                EnterBehavior = Activate;
                ExitBehavior = Deactivate;
                break;
            case Behaviors.StayDeactivate:
                EnterBehavior = Deactivate;
                ExitBehavior = Activate;
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ExitBehavior(other);
    }

    private void OnTriggerEnter(Collider other)
    {
        EnterBehavior(other);
    }
}
