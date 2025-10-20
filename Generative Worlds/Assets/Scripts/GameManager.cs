using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public CharacterControl player;

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void DisablePlayer()
    {
        if (player != null)
        {
            player.enabled = false;
        }
    }

    public void EnablePlayer()
    {
        if (player != null)
        {
            player.enabled = true;
        }
    }

    public void RespawnPlayer(Vector3 position)
    {
        if (player != null)
        {
            player.enabled = false;
            player.transform.position = position;
            player.velocity = Vector3.zero; // Reset velocity so player doesn't keep falling
            player.enabled = true;
        }
    }

    
    // public void StartSpawning()
    // {
    //     if (EnemySpawner != null)
    //         EnemySpawner.InvokeRepeating("SpawnRandomEnemy", 2f, 5f);
    // }

    // public void StopSpawning()
    // {
    //     if (EnemySpawner != null)
    //         EnemySpawner.CancelInvoke("SpawnRandomEnemy");
    // }
}
