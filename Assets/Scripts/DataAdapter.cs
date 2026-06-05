using UnityEngine;

public class RobotDataAdapter : RobotData
{
    public RobotDataAdapter(Robot robot)
    {
        this.position = robot.transform.position;
        this.rotation = robot.transform.rotation;
    }

    public RobotDataAdapter(Transform transform)
    {
        this.position = transform.position;
        this.rotation = transform.rotation;
    }
}
