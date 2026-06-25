using UnityEngine;

public class Fractures : MonoBehaviour
{
    public Transform meshes;
    public Transform centralPoint;
    public float force = 2;
#nullable enable
    public ParticleSystem? ps;
    public AudioSource? audioSource;

    public void Activate()
    {
        for (int i = 0; i < meshes.childCount; i++)
        {
            Transform child = meshes.GetChild(i);
            Rigidbody childRb = child.GetComponent<Rigidbody>();
            if (child.TryGetComponent<HitDestroy>(out HitDestroy hit))
                hit.enabledDestroy = true;
            childRb.isKinematic = false;
            Vector3 direction = child.localPosition - centralPoint.localPosition;
            direction.y = direction.y < 0 ? 0.3f : direction.y;
            childRb.linearVelocity = direction * force;
        }
    }
}
