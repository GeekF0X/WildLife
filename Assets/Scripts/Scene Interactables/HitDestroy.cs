using System;
using UnityEngine;

public class HitDestroy : MonoBehaviour
{
    #nullable enable
    public GameObject? hitObj;
    Animator? anim;
    Collider? coll;

    void Start()
    {
        TryGetComponent<Animator>(out anim);
        if(!TryGetComponent<Collider>(out coll))
        {
            throw new Exception(gameObject.name + " sem colisor!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (coll)
        {
            if (hitObj)
            {
                if (collision.gameObject == hitObj)
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
