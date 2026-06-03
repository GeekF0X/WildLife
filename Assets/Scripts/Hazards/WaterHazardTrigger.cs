using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WaterHazardTrigger : MonoBehaviour
{
    private WaterHazard parentHazard;

    private void Awake()
    {
        parentHazard = GetComponentInParent<WaterHazard>();
        var col = GetComponent<Collider>();
        if (col != null) col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (parentHazard != null)
            parentHazard.HandleTrigger(other);
    }
}