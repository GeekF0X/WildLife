using UnityEngine;

public class RobotAnimations : MonoBehaviour
{
    public Animator animator;

    public void SetMoving(bool moving)
    {
        animator.SetBool("Moving", moving);
    }

    public void TurnOn()
    {
        animator.SetBool("Power", true);
    }
    public void TurnOff()
    {
        animator.SetBool("Power", false);
    }

}
