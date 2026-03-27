using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEngine.UI.Image;

public class RobotSmall:Robot
{
    public Transform raycastOffset;
    public GameObject magnet;
    public float maxDistance = 10f;
    public float magnetSpeed = 14f;

    Vector3 magnetStart;
    Vector3 target;
    bool hasTarget;
    private void Start()
    {
        magnetStart = magnet.transform.localPosition;    
    }

    public override void TakeAction()
    {
        Ray rayTarget = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        Vector3 vectorOffset = raycastOffset.position - rayTarget.origin;

        float offset = Vector3.Dot(vectorOffset, rayTarget.direction);
        rayTarget.origin = rayTarget.origin + (Camera.main.transform.forward * offset);

        Debug.DrawRay(rayTarget.origin, rayTarget.direction * maxDistance, Color.red, 5);
        if (Physics.Raycast(rayTarget, out RaycastHit hit, maxDistance))
        {
            Debug.Log("Acertou: " + hit.collider.name);
            hasTarget = true;
            target = hit.point;
        }

    }

    public override void CancelAction()
    {
        throw new System.NotImplementedException();
    }

    new private void Update()
    {
        base.Update();

        bool distanceReached = (magnet.transform.position - transform.position).magnitude > maxDistance;
        if (hasTarget)
        {
            Vector3 direction = target - magnet.transform.position;
            if (distanceReached || direction.magnitude < 0.2f)
                hasTarget = false;
            magnet.transform.position += direction * magnetSpeed * Time.deltaTime;
        }
        else
        {
            if (magnet.transform.localPosition != magnetStart)
            {
                magnet.transform.position += (magnetStart - magnet.transform.localPosition) * magnetSpeed * Time.deltaTime;
            }
        }
    }
}
