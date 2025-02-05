using UnityEngine;

public class CamMover : MonoBehaviour
{
    public float movementSpeed = 10f; // Speed of camera movement
    public float lookSpeed = 2f; // Speed of camera rotation
    public float fastSpeedMultiplier = 2f; // Speed multiplier for fast movement (e.g., when holding Shift)

    private Vector3 lastMousePosition;

    void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    void HandleMovement()
    {
        float speed = movementSpeed;

        // Hold Left Shift to move faster
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            speed *= fastSpeedMultiplier;
        }

        Vector3 direction = new Vector3();

        // WASD keys for forward, backward, and lateral movement
        if (Input.GetKey(KeyCode.W)) direction += transform.forward;
        if (Input.GetKey(KeyCode.S)) direction -= transform.forward;
        if (Input.GetKey(KeyCode.A)) direction -= transform.right;
        if (Input.GetKey(KeyCode.D)) direction += transform.right;

        // Q and E keys for vertical movement
        if (Input.GetKey(KeyCode.Q)) direction -= transform.up;
        if (Input.GetKey(KeyCode.E)) direction += transform.up;

        transform.position += direction * speed * Time.deltaTime;
    }

    void HandleRotation()
    {
        if (Input.GetMouseButton(1)) // Right mouse button for rotation
        {
            Vector3 mouseDelta = Input.mousePosition - lastMousePosition;

            float yaw = mouseDelta.x * lookSpeed * Time.deltaTime;
            float pitch = -mouseDelta.y * lookSpeed * Time.deltaTime;

            transform.eulerAngles += new Vector3(pitch, yaw, 0);
        }

        lastMousePosition = Input.mousePosition;
    }
}
