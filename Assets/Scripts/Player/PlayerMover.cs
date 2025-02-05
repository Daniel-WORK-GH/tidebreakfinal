using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{    
    public float walkingSpeed = 5f;
    public float runningSpeed = 8f;

    public float maxRunTime = 30;
    public float reminingRunTime = 30;

    public float jumpForce = 5;
    private bool isGrounded = false;

    public Camera Camera;

    private Rigidbody rb;
    private PlayerShipHandler psh;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        psh = GetComponent<PlayerShipHandler>();

        reminingRunTime = maxRunTime;
    }

    void Update()
    {
        if(psh.inShip) return;

        // Find forward back left right vectors
        Vector3 forward = Camera.transform.forward;
        Vector3 right = Camera.transform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        // Decide move speed
        var speed = Input.GetKey(KeyCode.LeftShift) ? runningSpeed : walkingSpeed;

        // Limit running
        reminingRunTime += Input.GetKey(KeyCode.LeftShift) ? -Time.deltaTime : Time.deltaTime * 2;
        if (reminingRunTime < 0) 
        {
            reminingRunTime = 0;
            speed = walkingSpeed;
        }
        if (reminingRunTime > maxRunTime)
        {
            reminingRunTime = maxRunTime;
        }

        // Handle moving
        rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        Vector3 vel = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            vel += forward * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            vel += - forward * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            vel += - right * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            vel += right * Time.deltaTime * speed;
        }

        rb.AddForce(vel, ForceMode.VelocityChange);

        // Handle jump
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = -transform.up;
        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, 10))
        {
            if (hit.distance < 1.1f)
            {
                isGrounded = true;
            }
            else 
            {
                isGrounded = false;
            }
        }
        else
        {
            isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }
}