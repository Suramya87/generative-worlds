using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public float gravity = -9.81f;
    public LayerMask groundLayer;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    private PlayerInputHandler input;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        input = GetComponent<PlayerInputHandler>();
    }

    private void Update()
    {
        HandleMovement();
        HandleJumpAndGravity();
    }

    void HandleMovement()
    {
        Vector2 moveInput = input.MoveInput;

        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(move * moveSpeed * Time.deltaTime);
    }

    void HandleJumpAndGravity()
    {
        isGrounded = CheckGrounded();

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (input.JumpPressed && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    bool CheckGrounded()
    {
        float rayLength = 1f;
        Vector3 origin = transform.position + Vector3.down * 0.1f;
        return Physics.Raycast(origin, Vector3.down, rayLength, groundLayer);
    }
}
