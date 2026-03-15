using UnityEngine;
using UnityEngine.InputSystem;

public class RobotBig:Robot
{
    protected override void TakeAction()
    {
        Debug.Log("Carregar!");
    }

    protected override void CancelAction()
    {
        throw new System.NotImplementedException();
    }
}
