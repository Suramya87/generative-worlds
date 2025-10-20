using UnityEngine;

public class EnemyReturnNotifier : MonoBehaviour
{
    public EnemySpawner spawner;

    private void OnDisable()
    {
        // when the enemy is disabled (returned to pool), inform spawner
        if (spawner != null) spawner.NotifyEnemyReturned();
    }
}
