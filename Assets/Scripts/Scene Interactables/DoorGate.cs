using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class DoorGate : MonoBehaviour
{
    public Animator doorAnimator;
    public GameObject lever;
    public bool onTrigger = false;

    public UnityEvent ActivationEvent;

    void Start()
    {
        doorAnimator.SetFloat("Blend", 0);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == lever)
        {
            onTrigger = true;
            float blend = doorAnimator.GetFloat("Blend");
            blend = Mathf.Clamp(blend + Time.deltaTime * 0.33f, 0, 1);
            doorAnimator.SetFloat("Blend", blend);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == lever)
        {
            ActivationEvent.Invoke();
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == lever)
        {
            onTrigger = false;
        }
    }

    private void Update()
    {
        if (!onTrigger)
        {
            float blend = doorAnimator.GetFloat("Blend");
            blend = Mathf.Clamp(blend - Time.deltaTime * 0.33f, 0, 1);
            doorAnimator.SetFloat("Blend", blend);
        }
    }
}
