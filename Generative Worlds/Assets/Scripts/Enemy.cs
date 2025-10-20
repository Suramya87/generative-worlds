using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyType enemyType;
    public int maxHealth = 100;
    protected int currentHealth;

    protected virtual void OnEnable()

    {
        currentHealth = maxHealth;
    }

    public virtual void OnSpawn(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
        currentHealth = maxHealth;
    }

    public virtual void OnDespawn()
    {
        gameObject.SetActive(false);
    }

    public virtual void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
            Die();
    }

    protected virtual void Die()
    {
        // Let spawner/pool manager handle return. We'll find the pool manager and return.
        EnemyPoolManager.Instance?.ReturnEnemy(enemyType, gameObject);
    }
}
