using Unity.Cinemachine;
using UnityEngine;

public class RobotSmall:Robot
{
    public IStates state { get; private set; }

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

    protected override void EnterAim()
    {
        if (aimCamera != null)
        {
            aimCamera.gameObject.SetActive(true);
            Move = AimMove;
        }
    }
    protected override void ExitAim()
    {     
        if (aimCamera != null)
        {
            if (aimCamera.gameObject.activeSelf)
            {
                aimCamera.gameObject.SetActive(false);
                if(state.GetName() != "Carrying")
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
            Vector3 moveVector = (forward * moveDirection.z + camera.right * moveDirection.x) * Time.fixedDeltaTime * speed/3;
            controller.Move(moveVector);

            transform.rotation = Quaternion.LookRotation(forward);
        }
    }

    public void ResetTransform(bool freezeHook = true)
    {
        magnet.rb.isKinematic = freezeHook;
        transform.rotation = Quaternion.identity;
        magnet.hookControl.spring = magnet.pullForce;
        magnet.transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
        magnet.transform.localPosition = magnetStart;
        magnet.rb.linearVelocity = Vector3.zero;
    }

    private void Update()
    {
        state.Update();
        if((state.GetName() != "Idle" && state.GetName() != "Carrying") && isEnergized)
        {
            isEnergized = false;
        }
    }
    protected override void Efeitotrocar()
    {
        if (_valorAtual < 2.2f)
        {
            _valorAtual += Time.deltaTime * velocidade;
            _valorAtual = Mathf.Min(_valorAtual, 2.2f);
            ren.material.SetFloat("_time", _valorAtual);
            if(_valorAtual == 2.2f)
            {
                tocarefito = false;
                _valorAtual = 0f;
                ren.material.SetFloat("_direfeito", 1);
            }
        }
    }
}
