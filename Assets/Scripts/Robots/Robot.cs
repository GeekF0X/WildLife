using Unity.Cinemachine;
using UnityEngine;
using static Unity.Cinemachine.CinemachineImpulseManager.ImpulseEvent;

public abstract class Robot : MonoBehaviour
{
    public CharacterController controller;
    public bool isEnergized;
    public float speed = 5f;
    public float gravity = -8f;
    Vector3 moveDirection = new();

    public float fall;
    public Robot other;
    public CinemachineCamera cineCamera;
    Transform lastCameraLook = null;

    public int climbSpeed = 3;
    public float dist = 1.5f;
    public LayerMask layerMask;
    public bool isClimbing;

    public void Change()
    {
        lastCameraLook = cineCamera.transform;

        isEnergized = false;
        moveDirection = Vector3.zero;
        cineCamera.enabled = false;

        other.cineCamera.enabled = true;
        Invoke("EnergyOther", Camera.main.GetComponent<CinemachineBrain>().DefaultBlend.Time);
        if(other.lastCameraLook != null)
            other.cineCamera.ForceCameraPosition(other.lastCameraLook.position, other.lastCameraLook.rotation);
    }

    void EnergyOther()
    {
        other.isEnergized = true;
    }

    protected void Update()
    {
        if (isEnergized)
        {
            Transform camera = Camera.main.transform;

            Vector3 forward = camera.forward;
            forward.y = 0;
            forward.Normalize();

            Vector3 moveVector = (forward * moveDirection.z + camera.right * moveDirection.x) * Time.deltaTime * speed;

            controller.Move(moveVector);

            transform.rotation = Quaternion.LookRotation(forward);

        if (controller.isGrounded || isClimbing)
            fall = 0;
        else
            fall += gravity * Time.deltaTime;

        controller.Move(Vector3.up * fall);

        if (isClimbing)
        {
            RaycastHit hit;

            if (! Physics.Raycast((transform.position - Vector3.up * 0.65f), transform.forward, out hit, dist, layerMask))
            {
                isClimbing = false;
            }
            controller.Move(Vector3.up * climbSpeed * Time.deltaTime);
        }

        }
        
    }

    public void Move(Vector2 input)
    {
        if (isEnergized)
        {
            moveDirection = new Vector3(input.x, 0, input.y);
        }
    }
    public void run(float r)
    {
        if (isEnergized)
        {
            speed = r;
        }
    }
    public void dash()
    {
        if (isEnergized)
        {
            Transform camera = Camera.main.transform;

            Vector3 forward = camera.forward;
            forward.y = 0;
            forward.Normalize();

            Vector3 moveVector = (forward * moveDirection.z + camera.right * moveDirection.x) * Time.deltaTime * speed;

            controller.Move(moveVector*20f);
        }
    }

    public abstract void TakeAction();
    public abstract void CancelAction();

    public void Climb(bool climbPressed)
    {
        if (isEnergized)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, dist, layerMask))
            {

                Debug.Log("Funciona!");

                isClimbing = climbPressed;

            } else isClimbing = false;

        }
    }
}
