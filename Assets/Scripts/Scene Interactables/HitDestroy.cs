using System;
using System.Collections.Generic;
using UnityEngine;

public class HitDestroy : MonoBehaviour
{
    public enum Behaviors { OnHit, OnThrow, Collectible }
    public Behaviors behavior;
    public List<GameObject> hitObj;

    #nullable enable
    Animator? anim;
    Collider? coll;
    Vector3 initialPosition;

    public bool enabledDestroy = true;
    void Start()
    {
        TryGetComponent<Animator>(out anim);
        if(!TryGetComponent<Collider>(out coll))
        {
            throw new Exception(gameObject.name + " sem colisor!");
        }

        if (behavior == Behaviors.OnThrow)
        {
            enabledDestroy = false;
            initialPosition = transform.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!enabledDestroy)
            return;

        if (behavior == Behaviors.OnThrow)
        {
            if (collision.gameObject.CompareTag("Player"))
                return;
            else
            {
                GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
                transform.localPosition = initialPosition;
                enabledDestroy = false;
                return;
            }
        } 

        if (coll)
        {
            if (behavior == Behaviors.Collectible && collision.gameObject.CompareTag("Player"))
            {
                coll.enabled = false;
                anim?.SetTrigger("Hit");
                return;
            }
            if (hitObj.Count > 0)
            {
                if ( hitObj.Contains(collision.gameObject))
                {
                    coll.enabled = false;
                    anim?.SetTrigger("Hit");
                }
            }
            else
            {
                coll.enabled = false;
                anim?.SetTrigger("Hit");
            }
        }
    }
}
