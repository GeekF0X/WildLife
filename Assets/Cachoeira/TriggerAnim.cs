using UnityEngine;
using UnityEngine.Events;

public class TriggerAnim : MonoBehaviour
{
#nullable enable
    public Animator? anim;

    private void OnTriggerExit(Collider other)
    {
        anim?.SetBool("Blocked", false);
        anim?.ResetTrigger("Stop");
    }

    private void OnTriggerEnter(Collider other)
    {
        anim?.SetBool("Blocked", true);
        anim?.ResetTrigger("Stop");
        anim?.SetTrigger("Stop");
    }
}
