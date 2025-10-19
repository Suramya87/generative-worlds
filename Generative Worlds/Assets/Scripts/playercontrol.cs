using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterControl : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public float gravity = -9.81f;

    [Header("Mouse Look")]
    public float mouseSensitivity = 200f;

    [Header("Ground Check")]
    public LayerMask groundLayer;

    private CharacterController controller;
    public Vector3 velocity;
    private bool isGrounded;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;

        // Optional: Register with GameManager (if not already assigned manually)
        if (GameManager.Instance != null && GameManager.Instance.player == null)
        {
            GameManager.Instance.player = this;
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleMouseLook();
        HandleJumpAndGravity();

        // Example: Auto-respawn if player falls below world
        if (transform.position.y < -10f)
        {
            GameManager.Instance.RespawnPlayer(Vector3.zero); // Replace with actual spawn point
        }
    }

    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up * mouseX * mouseSensitivity * Time.deltaTime);
    }

    void HandleJumpAndGravity()
    {
        isGrounded = IsGrounded();
        Debug.Log($"Is Grounded: {isGrounded}");

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private bool IsGrounded()
    {
        float rayLength = 1f;
        Vector3 rayOrigin = transform.position + Vector3.down * 0.1f;
        return Physics.Raycast(rayOrigin, Vector3.down, rayLength, groundLayer);
    }
}
