using UnityEngine;

public class RobotAnimations : MonoBehaviour
{
    public Animator animator;

    public void SetMoving(bool moving)
    {
        animator.SetBool("Moving", moving);
    }

    public void PowerButton()
    {
        animator.SetTrigger("Power");
    }
}
