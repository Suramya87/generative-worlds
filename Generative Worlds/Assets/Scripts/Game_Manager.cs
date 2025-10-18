using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerController player;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void DisablePlayer()
    {
        player.enabled = false;
    }

    public void RespawnPlayer(Vector3 position)
    {
        player.transform.position = position;
        player.enabled = true;
    }
}
