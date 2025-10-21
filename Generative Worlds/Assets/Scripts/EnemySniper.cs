using UnityEngine;

public class EnemySniper : Enemy
{
    public GameObject bulletPrefab; 
    public float shootInterval = 2f; 
    public float bulletSpawnOffset = 1.5f; 

    private Transform player;
    private float shootTimer;

    void Awake()
    {
        enemyType = EnemyType.Sniper;
        maxHealth = 50;
    }

    void Update()
    {
        // Debug.Log("Sniper update running");
        if (!gameObject.activeInHierarchy) return;

        if (player == null)
        {
            var foundPlayer = GameObject.FindGameObjectWithTag("Player");
            if (foundPlayer) player = foundPlayer.transform;
        }

        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0f; 
            transform.rotation = Quaternion.LookRotation(direction);

            shootTimer += Time.deltaTime;
            if (shootTimer >= shootInterval)
            {
                Shoot();
                shootTimer = 0f;
            }
        }

    }

    private void Shoot()
    {
        if (bulletPrefab == null) return;

        Vector3 spawnPos = transform.position + transform.forward * bulletSpawnOffset;
        GameObject bullet = Instantiate(bulletPrefab, spawnPos, transform.rotation);

    }

    public override void OnSpawn(Vector3 position)
    {
        base.OnSpawn(position);
        shootTimer = 0f;
    }

    protected override void Die()
    {
        base.Die();
    }
}
