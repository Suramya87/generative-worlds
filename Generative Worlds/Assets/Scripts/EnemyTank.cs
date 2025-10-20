using UnityEngine;

public class EnemyTank : Enemy
{
    void Awake()
    {
        enemyType = EnemyType.Tank;
        maxHealth = 50; 
    }

    public override void OnSpawn(Vector3 position)
    {
        base.OnSpawn(position);
    }

    protected override void Die()
    {

        base.Die();
    }
}
