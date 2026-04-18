using UnityEngine;

public class MagnetHook : MonoBehaviour
{
    public SpringJoint hookControl;
    public float pullForce = 100f;
    public float maxPullableMass = 49f;
    public float magnetSpeed = 14f;
    public float playerPullSpeed = 12f;
    public float maxDistance = 10f;

    public bool pullself = false;
    public bool hit = false;
    
    private void OnCollisionEnter(Collision collision)
    {
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
                pullself = false;
                hookControl.spring = pullForce;
                hookControl.maxDistance = 0.01f;
            }
        }
        else
        {
            hookControl.spring = pullForce*3;
            hookControl.maxDistance = 0.01f;
        }
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
            GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            
            if (GetComponent<FixedJoint>().connectedBody)
                GetComponent<FixedJoint>().connectedBody.linearVelocity = Vector3.zero;
            Destroy(GetComponent<FixedJoint>());
        }
        if (hookControl.spring == 0)
        {
            GetComponent<Rigidbody>().linearVelocity = Vector3.zero;

            hookControl.spring = pullForce * 3;
            hookControl.maxDistance = 0.1f;
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
