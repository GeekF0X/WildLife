using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public abstract class Robot : MonoBehaviour
{
    public CharacterController controller;
    public bool isEnergized;
    public float speed = 5f;
    public float gravity = -8f;
    protected Vector3 moveDirection = new();

    public float fall;
    public Robot other;
    public CinemachineCamera cineCamera;
    protected Transform lastCameraLook = null;
    protected UnityAction Move;

    public void Change()
    {
        CinemachineBrain brain = Camera.main.GetComponent<CinemachineBrain>();
        brain.DefaultBlend.Time = 2;
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
        CinemachineBrain brain = Camera.main.GetComponent<CinemachineBrain>();
        brain.DefaultBlend.Time = 0.5f;
        other.isEnergized = true;
    }

    protected void Update()
    {
        Move();
        Fall();
    }

    protected void Start()
    {
        Move = BaseMove;
    }

    public void MoveInput(Vector2 input)
    {
        if (isEnergized)
        {
            moveDirection = new Vector3(input.x, 0, input.y);
        }
    }
    protected void BaseMove()
    {
        if (isEnergized)
        {
            Transform camera = Camera.main.transform;

            Vector3 forward = camera.forward;
            forward.y = 0;
            forward.Normalize();

            Vector3 moveVector = (forward * moveDirection.z + camera.right * moveDirection.x) * Time.deltaTime * speed;

            controller.Move(moveVector);

            if(moveVector.magnitude > 0)
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveVector), 0.08f);
        }
    }

    void Fall() 
    {
        controller.Move(Vector3.up * fall);

        if (controller.isGrounded)
            fall = 0;
        else
            fall += gravity * Time.deltaTime;
    }

    public abstract void TakeAction();
    public abstract void CancelAction();

    public abstract void Aim(bool shouldAim);
}
