using UnityEngine;
using UnityEngine.InputSystem;

public class RobotBig:Robot
{
    float maxDistance = 1.4f;
    public override void TakeAction()
    {
        Ray rayTarget = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        rayTarget.origin = transform.position;
        Debug.DrawRay(rayTarget.origin, rayTarget.direction * maxDistance, Color.red, 5);
        if (Physics.Raycast(rayTarget, out RaycastHit hit, maxDistance))
        {
            Debug.Log("Acertou: " + hit.collider.name);
        }
    }

    public override void CancelAction()
    {
        throw new System.NotImplementedException();
    }
}
