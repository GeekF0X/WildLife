using UnityEngine;
using UnityEngine.InputSystem;

public class RobotManager : MonoBehaviour
{
    public enum RobotType { SMALL, BIG}
    public Robot small;
    public Robot big;

    Robot controlledRobot;
    RobotType robot = RobotType.SMALL;
    bool action = false;
    private void Start()
    {
        controlledRobot = small;
        small.isEnergized = true;
        small.cineCamera.enabled = true;

        big.isEnergized = false;
        big.cineCamera.enabled = false;
    }

    public void OnChange(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            controlledRobot.Change();

            robot = robot == RobotType.SMALL ? RobotType.BIG : RobotType.SMALL;
            controlledRobot = robot == RobotType.SMALL ? small : big;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        controlledRobot.MoveInput(context.ReadValue<Vector2>());
    }

    public void OnTakeAction(InputAction.CallbackContext context)
    {
        if (context.performed && !action)
        {
            controlledRobot.TakeAction();
            action = true;
        }
        else if (!context.performed && action)
        {
            controlledRobot.CancelAction();
            action = false;
        }

    }

    public void OnAim(InputAction.CallbackContext context)
    {
        controlledRobot.Aim(context.performed);
    }
}
