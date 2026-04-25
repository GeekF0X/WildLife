using UnityEngine;
using UnityEngine.InputSystem;

public class ClimbController : MonoBehaviour
{
    public int climbSpeed = 3;
    public float dist = 1.5f;
    public LayerMask layerMask;
    public CharacterController cc;
    public bool isClimbing;
    public Vector2 moveInput;
    public bool climbPressed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Climb();
        Debug.DrawRay(transform.position, transform.forward * dist, Color.red);
        if (isClimbing)
        {
            Vector3 move = transform.up * moveInput.y * climbSpeed;
            cc.Move(move * Time.deltaTime);
        }
    }

    void Climb()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, dist, layerMask))
        {
            if (climbPressed)
            {
            isClimbing = true;
            }
        } else isClimbing = false;
    }
}
