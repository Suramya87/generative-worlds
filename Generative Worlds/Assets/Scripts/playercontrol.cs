using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterControl : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public float gravity = -9.81f;

    [Header("Mouse Look")]
    public float mouseSensitivity = 1000f;

    [Header("Ground Check")]
    public LayerMask groundLayer;

    [Header("Shooting")]
    public GameObject bulletPrefab; // assign your bullet prefab
    public float bulletSpeed = 20f;
    public Transform firePoint; // point from where bullet is shot

    private CharacterController controller;
    public Vector3 velocity;
    private bool isGrounded;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (GameManager.Instance != null && GameManager.Instance.player == null)
            GameManager.Instance.player = this;
    }

    private void Update()
    {
        HandleMouseLook();
        HandleMovement();
        HandleJumpAndGravity();
        HandleShooting();

        // Simple respawn check
        if (transform.position.y < -10f)
            GameManager.Instance.RespawnPlayer(Vector3.zero);
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        // Rotate player left/right only (yaw)
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);
    }

    void HandleJumpAndGravity()
    {
        isGrounded = IsGrounded();

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private bool IsGrounded()
    {
        float rayLength = 1f;
        Vector3 rayOrigin = transform.position + Vector3.down * 0.1f;
        return Physics.Raycast(rayOrigin, Vector3.down, rayLength, groundLayer);
    }

    void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0)) // Left click
        {
            // Debug.Log("Left click detected!");

            Shoot();
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = firePoint.forward * bulletSpeed; // fixed property
            }

            // Debug message to confirm shooting
            // Debug.Log("Bullet fired at position: " + firePoint.position + " with direction: " + firePoint.forward);
        }
        else
        {
            // Debug.LogWarning("Bullet prefab or firePoint is not assigned!");
        }
    }


}
