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

    public float jumpForce = 1f;
    int jumpCount = 0;
    public int maxJumps = 2;

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
            Move();

            if (isClimbing)
            {
                Climb();
            }
       
        }
        Fall();
    }

    public void MoveInput(Vector2 input)
    {
        if (isEnergized)
        {
            moveDirection = new Vector3(input.x, 0, input.y);
        }
    }
    void Move()
    {
        Transform camera = Camera.main.transform;

        Vector3 forward = camera.forward;
        forward.y = 0;
        forward.Normalize();

        Vector3 moveVector = (forward * moveDirection.z + camera.right * moveDirection.x) * Time.deltaTime * speed;

        controller.Move(moveVector);

        transform.rotation = Quaternion.LookRotation(forward);
    }
    void Fall()
    {
        controller.Move(Vector3.up * fall * Time.deltaTime);

        if (controller.isGrounded || isClimbing)
        {
            fall = -0.2f; 
            jumpCount = 0;
        }
        else
        {
            fall += gravity * Time.deltaTime;
        }
    }

    public void ClimbInput(bool climbPressed)
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
    void Climb()
    {
        RaycastHit hit;

        if (!Physics.Raycast((transform.position - Vector3.up * 0.65f), transform.forward, out hit, dist, layerMask))
        {
            isClimbing = false;
        }
        controller.Move(Vector3.up * climbSpeed * Time.deltaTime);
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

            controller.Move(moveVector * 20f);
        }
    }
    public void JumpInput()
    {
        if (!isEnergized) return;

        if (jumpCount < maxJumps)
        {
            fall = jumpForce; 
            jumpCount++;
        }
    }

    public abstract void TakeAction();
    public abstract void CancelAction();

}
