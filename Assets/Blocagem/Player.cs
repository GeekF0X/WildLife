using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed = 7f;
    public float jump = 10f;
    public float gravity = -10f;
    CharacterController cc;
    Transform cameraDir;
    Vector2 directionInput;
    Vector3 directionMove;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        cameraDir = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (cc.isGrounded)
            directionMove.y += -0.1f * Time.deltaTime;
        else
            directionMove.y += gravity * Time.deltaTime;

        Vector3 dir = cameraDir.forward;
        dir.y = 0;
        dir = dir.normalized;
        dir = dir * directionMove.z;
        dir += cameraDir.right * directionMove.x;
        dir.y = directionMove.y;

        dir = dir * speed * Time.deltaTime;
        GetComponent<CharacterController>().Move(dir);
        dir.y = 0;
        if (dir != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), 0.08f);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        directionInput = context.ReadValue<Vector2>();
        directionInput = directionInput.normalized;
        directionMove.z = directionInput.y;
        directionMove.x = directionInput.x;
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && cc.isGrounded)
        {
            directionMove.y = jump;
        }
    }
}