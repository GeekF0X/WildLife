using UnityEngine;

public class RobotThrowableAdapter : ThrowableObject
{
    public RobotSmall robot;

    //override public void OnPickedUp(Transform holdPoint)
    //{
    //    base.OnPickedUp(holdPoint);
    //    robot.gravity = 0;
    //}
    private void Start()
    {
        rb.freezeRotation = true;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Player")
        {
            robot.transform.SetParent(null);
            robot.controller.enabled = true;
            robot.ResetTransform();

            Destroy(this.gameObject);
        }
    }

    public void RobotSet(RobotSmall robot)
    {
        this.robot = robot;
        robot.transform.SetParent(this.transform);
        robot.transform.localPosition = Vector3.zero;
        robot.controller.enabled = false;
        robot.ResetTransform();
    }
}
