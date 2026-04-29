using Unity.Cinemachine;
using UnityEngine;

public class RobotSmall:Robot
{
    IStates state;

    public CinemachineCamera aimCamera;
    public Transform raycastOffset;
    public MagnetHook magnet;

    public Vector3 magnetStart;
    
    public Vector3 directionFall;
    public float selfGravity;
    private new void Start()
    {
        base.Start();
        aimCamera.gameObject.SetActive(false);
        ChangeState(new RobotSmallIdle(this));
        selfGravity = gravity;
        magnetStart = magnet.gameObject.transform.localPosition;
    }

    public void ChangeState(IStates state)
    {
        this.state?.Exit();
        this.state = state;
        this.state.Enter();
    }

    public override void TakeAction()
    {
        if(state.GetName() == "Idle" && aimCamera.gameObject.activeSelf)
        {
            //ExitAim();
            ChangeState(new RobotSmallShoot(this));
        }
    }

    public override void CancelAction()
    {
        if (state.GetName() == "PullSelf")
        {
            ChangeState(new RobotSmallInertial(this));
        }
        else if(state.GetName() != "Inertial")
        {
            ChangeState(new RobotSmallRetract(this));
        }
    }

    public override void Aim(bool s)
    {
        if (s)
            EnterAim();
        else
            ExitAim();
    }

    void EnterAim()
    {
        if (aimCamera != null)
        {
            aimCamera.gameObject.SetActive(true);
            Move = AimMove;
        }
    }
    void ExitAim()
    {
        
        if (aimCamera != null)
        {
            if (aimCamera.gameObject.activeSelf)
            {
                aimCamera.gameObject.SetActive(false);
                Move = BaseMove;
            }

        }
    }
    void AimMove()
    {
        if (isEnergized)
        {
            Transform camera = Camera.main.transform;

            Vector3 forward = camera.forward;
            forward.y = 0;
            forward.Normalize();
            Vector3 moveVector = (forward * moveDirection.z + camera.right * moveDirection.x) * Time.deltaTime * speed/3;
            controller.Move(moveVector);

            transform.rotation = Quaternion.LookRotation(forward);
        }
    }

    new private void Update()
    {
        base.Update();

        state.Update();
    }
}
