using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
    public static EnemyPoolManager Instance { get; private set; }

    [System.Serializable]
    public class EnemyPool
    {
        public EnemyType type;
        public GameObject prefab;
        public int poolSize = 5;
        [HideInInspector] public Queue<GameObject> poolQueue = new Queue<GameObject>();
    }

    public List<EnemyPool> enemyPools = new List<EnemyPool>();

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        foreach (var pool in enemyPools)
        {
            for (int i = 0; i < pool.poolSize; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                pool.poolQueue.Enqueue(obj);
                obj.transform.SetParent(transform);
            }
        }
    }

    public GameObject GetEnemy(EnemyType type)
    {
        var pool = enemyPools.Find(p => p.type == type);
        if (pool == null) return null;

        GameObject enemyObj = null;
        if (pool.poolQueue.Count > 0)
        {
            enemyObj = pool.poolQueue.Dequeue();
        }
        else
        {
            enemyObj = Instantiate(pool.prefab);
        }

        enemyObj.SetActive(true);
        var enemyComp = enemyObj.GetComponent<Enemy>();
        enemyComp?.OnSpawn(enemyObj.transform.position); 
        return enemyObj;
    }

    public void ReturnEnemy(EnemyType type, GameObject enemy)
    {
        var pool = enemyPools.Find(p => p.type == type);
        if (pool == null)
        {
            Destroy(enemy);
            return;
        }

        var enemyComp = enemy.GetComponent<Enemy>();
        enemyComp?.OnDespawn();

        enemy.SetActive(false);
        pool.poolQueue.Enqueue(enemy);
        enemy.transform.SetParent(transform);
    }
}
