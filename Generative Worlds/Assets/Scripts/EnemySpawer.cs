using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public float spawnInterval = 5f;
    public int maxEnemies = 10;

    private int currentEnemyCount = 0;

    void Start()
    {
        InvokeRepeating(nameof(SpawnRandomEnemy), 2f, spawnInterval);
    }

    void SpawnRandomEnemy()
    {
        if (currentEnemyCount >= maxEnemies) return;
        if (spawnPoints == null || spawnPoints.Length == 0) return;

        EnemyType randomType = (EnemyType)Random.Range(0, System.Enum.GetValues(typeof(EnemyType)).Length);
        Transform randomSpawn = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject enemyObj = EnemyPoolManager.Instance.GetEnemy(randomType);
        if (enemyObj != null)
        {
            enemyObj.transform.position = randomSpawn.position;
            enemyObj.transform.rotation = randomSpawn.rotation;
            currentEnemyCount++;

            // Attach a small helper to reduce count when returned to pool
            var e = enemyObj.GetComponent<EnemyReturnNotifier>();
            if (e == null) enemyObj.AddComponent<EnemyReturnNotifier>().spawner = this;
            else e.spawner = this;
        }
    }

    public void NotifyEnemyReturned()
    {
        currentEnemyCount = Mathf.Max(0, currentEnemyCount - 1);
    }
}
