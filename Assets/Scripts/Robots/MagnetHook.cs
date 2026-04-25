using UnityEngine;

public class MagnetHook : MonoBehaviour
{
    public SpringJoint hookControl;
    public float pullForce = 100f;
    public float maxPullableMass = 10f;
    public float magnetSpeed = 14f;
    public float playerPullSpeed = 12f;
    public float maxDistance = 10f;

    public bool pullself = false;
    public bool hit = false;

    public Rigidbody rb;

    public int colliding = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        colliding++;
        hit = true;
        GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        if (collision.gameObject.tag == "Hookable" && hookControl.spring == 0)
        {
            gameObject.AddComponent<FixedJoint>();
            GetComponent<FixedJoint>().connectedBody = collision.rigidbody;
            if(collision.rigidbody.mass > maxPullableMass || collision.rigidbody.isKinematic)
            {
                pullself = true;
            }
            else
            {
                Debug.Log(collision.rigidbody.mass > maxPullableMass);
                pullself = false;
                hookControl.spring = pullForce;
                hookControl.maxDistance = 0.001f;
            }
        }
        else
        {
            hookControl.spring = pullForce/7;
            hookControl.maxDistance = 0.001f;
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        colliding--;
    }

    public void ShootMagnet()
    {
        hookControl.maxDistance = maxDistance;
        hookControl.spring = 0;
        hit = false;
        pullself = false;
    }
    public void ReleaseHooked()
    {
        if(hasHooked())
        {
            rb.linearVelocity = Vector3.zero;
            
            if (GetComponent<FixedJoint>().connectedBody)
                GetComponent<FixedJoint>().connectedBody.linearVelocity = Vector3.zero;
            Destroy(GetComponent<FixedJoint>());
        }
        if (hookControl.spring == 0)
        {
            rb.linearVelocity = Vector3.zero;

            hookControl.spring = pullForce/7;
            hookControl.maxDistance = 0.001f;
        }
    }

    public bool hasHooked()
    {
        return GetComponent<FixedJoint>();
    }

    public void SetMaxDistance(float dist)
    {
        maxDistance = dist;
    }

    public Rigidbody GetHooked()
    {
        if (hasHooked())
        {
            return GetComponent<FixedJoint>().connectedBody;
        }
        else
            return null;
    }
}
