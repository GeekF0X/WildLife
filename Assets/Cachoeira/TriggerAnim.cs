using UnityEngine;
using UnityEngine.Events;

public class TriggerAnim : MonoBehaviour
{

    UnityAction<Collider> EnterBehavior = (Collider col) => { return; };
    UnityAction<Collider> ExitBehavior = (Collider col) => { return; };

    public Animator? anim;

    private void OnTriggerExit(Collider other)
    {
        anim?.SetBool("Blocked", false);
        ExitBehavior(other);
        anim.ResetTrigger("Stop");
    }

    private void OnTriggerEnter(Collider other)
    {
        anim?.SetBool("Blocked", true);
        anim.ResetTrigger("Stop");
        anim?.SetTrigger("Stop");
        Debug.Log("Colidiu " + other.tag);
        EnterBehavior(other);
    }
}
