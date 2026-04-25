using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class RobotBig : Robot
{
    public enum BigState { Idle, Holding, Aiming }
    public BigState currentState = BigState.Idle;

    [Header("Pickup")]
    [Tooltip("Transform que aponta pra frente do robô (peito).")]
    public Transform detectionOrigin;
    public float pickupRange  = 3f;
    public float pickupRadius = 0.75f;
    public LayerMask throwableMask;
    [Tooltip("Ponto acima da cabeça onde o objeto fica.")]
    public Transform holdPoint;

    [Header("Aim (Cinemachine)")]
    public CinemachineCamera aimCamera;
    public int aimPriority = 20;
    public int idlePriority = 0;

    [Header("Throw")]
    public Transform throwOrigin;
    public float throwSpeed     = 18f;
    public float maxPitch       = 60f;
    public float aimSensitivity = 2f;

    [Header("Trajectory")]
    public LineRenderer trajectoryLine;
    public int   trajectoryPoints      = 40;
    public float trajectoryTimeStep    = 0.05f;
    public LayerMask trajectoryCollisionMask = ~0;

    [Header("Animation")]
    public Animator animator;
    public float throwAnimDelay = 0.15f;

    private ThrowableObject heldObject;
    private float currentPitch;

    private void Start()
    {
        if (aimCamera != null) aimCamera.gameObject.SetActive(false);
        if (trajectoryLine != null)
        {
            trajectoryLine.enabled = false;
            trajectoryLine.positionCount = 0;
        }
    }

    //protected override bool ShouldFaceCamera() => currentState != BigState.Aiming;

    public override void TakeAction()
    {
        switch (currentState)
        {
            case BigState.Idle:    TryPickup();        break;
            case BigState.Aiming:  ThrowHeldObject();  break;
            case BigState.Holding: DropHeldObject();   break;
        }
    }

    public override void CancelAction()
    {

    }

    new private void Update()
    {
        if (!isEnergized)
        {
            base.Update();
            return;
        }

        bool rightHeld = Mouse.current != null && Mouse.current.rightButton.isPressed;

        if (rightHeld && currentState == BigState.Holding)
            EnterAim();
        else if (!rightHeld && currentState == BigState.Aiming)
            ExitAim();

        if (currentState == BigState.Aiming)
        {
            AimingMovement();
            UpdateAim();
        }
        else
        {
            base.Update();
        }
    }

    private void AimingMovement()
    {
        Vector2 input = Vector2.zero;
        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed) input.y += 1;
            if (Keyboard.current.sKey.isPressed) input.y -= 1;
            if (Keyboard.current.dKey.isPressed) input.x += 1;
            if (Keyboard.current.aKey.isPressed) input.x -= 1;
        }

        Vector3 move = (transform.forward * input.y + transform.right * input.x) * speed * Time.deltaTime;
        controller.Move(move);

        if (controller.isGrounded) fall = 0;
        else fall += gravity * Time.deltaTime;
        controller.Move(Vector3.up * fall);
    }

    private void TryPickup()
    {
        if (detectionOrigin == null || holdPoint == null) return;

        if (Physics.SphereCast(detectionOrigin.position, pickupRadius,
                               detectionOrigin.forward, out RaycastHit hit,
                               pickupRange, throwableMask,
                               QueryTriggerInteraction.Ignore))
        {
            var t = hit.collider.GetComponentInParent<ThrowableObject>();
            if (t != null && !t.IsHeld)
            {
                t.OnPickedUp(holdPoint);
                heldObject = t;
                currentState = BigState.Holding;
                if (animator != null) animator.SetBool("IsHolding", true);
            }
        }
    }

    private void EnterAim()
    {
        currentState = BigState.Aiming;
        currentPitch = 10f;

        if (aimCamera != null) aimCamera.gameObject.SetActive(true);
        if (trajectoryLine != null)
        {
            trajectoryLine.enabled = true;
            Debug.Log($"[EnterAim] trajectoryLine.enabled={trajectoryLine.enabled} gameObject ativo={trajectoryLine.gameObject.activeInHierarchy}");
        }
    }

    private void ExitAim()
    {
        currentState = BigState.Holding;

        if (aimCamera != null) aimCamera.gameObject.SetActive(false);
        if (trajectoryLine != null)
        {
            trajectoryLine.enabled = false;
            trajectoryLine.positionCount = 0;
        }
    }

    private void UpdateAim()
    {
        Vector2 mouseDelta = Mouse.current != null ? Mouse.current.delta.ReadValue() : Vector2.zero;

        transform.Rotate(0f, mouseDelta.x * aimSensitivity * 0.1f, 0f);

        currentPitch -= mouseDelta.y * aimSensitivity * 0.1f;
        currentPitch = Mathf.Clamp(currentPitch, -maxPitch, maxPitch);

        DrawTrajectory();
    }

    private Vector3 GetAimDirection()
    {
        Quaternion rot = transform.rotation * Quaternion.Euler(currentPitch, 0f, 0f);
        return rot * Vector3.forward;
    }

    private Vector3 GetThrowVelocity() => GetAimDirection() * throwSpeed;

    private void DrawTrajectory()
    {
        if (trajectoryLine == null || throwOrigin == null) return;

        Debug.Log($"[Draw] enabled={trajectoryLine.enabled} count será={trajectoryPoints} startPos={throwOrigin.position}");

        Vector3 startPos = throwOrigin.position;
        Vector3 velocity = GetThrowVelocity();
        Vector3 gravityV = Physics.gravity;

        trajectoryLine.positionCount = trajectoryPoints;
        Vector3 prev = startPos;
        int count = trajectoryPoints;

        for (int i = 0; i < trajectoryPoints; i++)
        {
            float t = i * trajectoryTimeStep;
            Vector3 pos = startPos + velocity * t + 0.5f * gravityV * t * t;

            if (i > 0 && Physics.Linecast(prev, pos, out RaycastHit hit,
                                          trajectoryCollisionMask,
                                          QueryTriggerInteraction.Ignore))
            {
                trajectoryLine.SetPosition(i, hit.point);
                count = i + 1;
                break;
            }

            trajectoryLine.SetPosition(i, pos);
            prev = pos;
        }
        trajectoryLine.positionCount = count;
    }

    private void ThrowHeldObject()
    {
        if (heldObject == null) return;

        Vector3 velocity = GetThrowVelocity();
        if (animator != null) animator.SetTrigger("Throw");
        StartCoroutine(ThrowCoroutine(heldObject, velocity));
        heldObject = null;

        ExitAim();
        currentState = BigState.Idle;
        if (animator != null) animator.SetBool("IsHolding", false);
    }

    private void DropHeldObject()
    {
        if (heldObject == null) return;
        heldObject.OnThrown(Vector3.zero);
        heldObject = null;
        currentState = BigState.Idle;
        if (animator != null) animator.SetBool("IsHolding", false);
    }

    private System.Collections.IEnumerator ThrowCoroutine(ThrowableObject obj, Vector3 velocity)
    {
        if (throwAnimDelay > 0f)
            yield return new WaitForSeconds(throwAnimDelay);
        obj.OnThrown(velocity);
    }

    private void OnDisable()
    {
        if (aimCamera != null) aimCamera.gameObject.SetActive(false);
        if (trajectoryLine != null) trajectoryLine.enabled = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (detectionOrigin == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(detectionOrigin.position + detectionOrigin.forward * pickupRange,
                              pickupRadius);
        Gizmos.DrawLine(detectionOrigin.position,
                        detectionOrigin.position + detectionOrigin.forward * pickupRange);
    }
}