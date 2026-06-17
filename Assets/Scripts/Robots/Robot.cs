using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

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
    public CinemachineCamera aimCamera;

    protected Transform lastCameraLook = null;
    protected UnityAction Move;

    public Renderer ren;
    [SerializeField] 
    public float velocidade = 1.0f;
    public float _valorAtual = 0.0f;
    public bool tocarefito = false;

    protected RobotAudio robotAudio;

    public void Change()
    {
        AtivarEfeitotroca();
        CinemachineBrain brain = Camera.main.GetComponent<CinemachineBrain>();
        brain.DefaultBlend.Time = 2;
        lastCameraLook = cineCamera.transform;

        if (TryGetComponent<RobotAnimations>(out RobotAnimations animations))
        {
            animations.CancelInvoke();
            animations.TurnOff();
        }
        isEnergized = false;
        moveDirection = Vector3.zero;
        cineCamera.enabled = false;
        ExitAim();

        if (robotAudio != null) robotAudio.StopMovementSound();

        other.cineCamera.enabled = true;
        if (other.TryGetComponent<RobotAnimations>(out RobotAnimations otherAnimations))
            otherAnimations.Invoke("TurnOn", 1.3f);
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

    void AtivarEfeitotroca()
    {
        tocarefito = true;
        ren.material.SetFloat("_direfeito", -1);
    }
   
    protected void FixedUpdate()
    {
        Move();
        Fall();
        HandleMovementSound();

        if (tocarefito)
        {
            Efeitotrocar();
        }
    }

    protected void Start()
    {
        Move = BaseMove;
        robotAudio = GetComponent<RobotAudio>();
    }

    private void HandleMovementSound()
    {
        if (robotAudio == null) return;
        bool isMoving = isEnergized 
                        && moveDirection.sqrMagnitude > 0.01f 
                        && controller.isGrounded;

        if (isMoving)
            robotAudio.StartMovementSound();
        else
            robotAudio.StopMovementSound();
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

            Vector3 moveVector = (forward * moveDirection.z + camera.right * moveDirection.x) * Time.fixedDeltaTime * speed;

            controller.Move(moveVector);

            if(moveVector.magnitude > 0)
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveVector), 15f * Time.fixedDeltaTime);
        }
    }

    void Fall() 
    {
        if(controller.enabled)
            controller.Move(Vector3.up * fall);

        if (controller.isGrounded)
            fall = 0;
        else
            fall += gravity * Time.fixedDeltaTime;
    }

    public abstract void TakeAction();
    public abstract void CancelAction();

    public abstract void Aim(bool shouldAim);
    protected abstract void EnterAim();
    protected abstract void ExitAim();
    protected abstract void Efeitotrocar();
}