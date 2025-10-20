using UnityEngine;

public class EnemySniper : Enemy
{
    void Awake()
    {
        enemyType = EnemyType.Sniper;
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
