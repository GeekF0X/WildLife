using UnityEngine;
using UnityEngine.UIElements;

public class RobotDataAdapter : RobotData
{
    public RobotDataAdapter(RobotData data)
    {
        this.position = data.position;
        this.rotation = data.rotation;
    }

    public RobotDataAdapter(Robot robot)
    {
        //this.position = robot.transform.position;
        //this.rotation = robot.transform.rotation;
    }

    public RobotDataAdapter(Transform transform)
    {
        this.position = transform.position;
        this.rotation = transform.rotation;
    }

    public Robot LoadRobot(ref Robot robot) 
    {
        robot.controller.enabled = false;
        robot.transform.position = position;
        robot.transform.rotation = rotation;
        robot.controller.enabled = true;
        return robot;
    }
}
