using UnityEngine;
using UnityEngine.Rendering;

public class DoorGate : MonoBehaviour
{
    public Animator doorAnimator;
    public GameObject lever;
    void Start()
    {
        doorAnimator.SetFloat("Blend", 0);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == lever)
        {
            float blend = doorAnimator.GetFloat("Blend");
            blend = Mathf.Clamp(blend + Time.deltaTime * 0.67f, 0, 1);
            doorAnimator.SetFloat("Blend", blend);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == lever)
        {
            float blend = doorAnimator.GetFloat("Blend");
            blend = Mathf.Clamp(blend - Time.deltaTime * 0.67f, 0, 1);
            doorAnimator.SetFloat("Blend", blend);
        }
    }
}
