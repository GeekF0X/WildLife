using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class ThrowableObject : MonoBehaviour
{
    [Tooltip("Massa considerada no cálculo da trajetória.")]
    public float throwableMass = 1f;

    private Rigidbody rb;
    private Collider col;

    public Rigidbody Rb => rb;
    public Collider Col => col;
    public bool IsHeld { get; private set; }

    void Awake()
    {
        rb  = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        rb.mass = throwableMass;
    }

    public void OnPickedUp(Transform holdPoint)
    {
        IsHeld = true;
        rb.isKinematic = true;
        col.enabled   = false;

        transform.SetParent(holdPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void OnThrown(Vector3 initialVelocity)
    {
        IsHeld = false;
        transform.SetParent(null);

        rb.isKinematic = false;
        col.enabled   = true;
        rb.linearVelocity  = initialVelocity;
        rb.angularVelocity = Random.insideUnitSphere * 2f;
    }
}