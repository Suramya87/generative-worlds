using UnityEngine;

public class PlayerCameraLook : MonoBehaviour
{
    public float mouseSensitivity = 200f;
    private PlayerInputHandler input;

    private void Start()
    {
        input = GetComponent<PlayerInputHandler>();
    }

    private void Update()
    {
        float mouseX = input.MouseX;
        transform.Rotate(Vector3.up * mouseX * mouseSensitivity * Time.deltaTime);
    }
}
