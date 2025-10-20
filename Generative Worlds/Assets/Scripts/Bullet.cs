using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object has an Enemy component
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            // Print debug message
            Debug.Log("Bullet hit enemy: " + enemy.name + " of type " + enemy.enemyType);

            // One-shot kill
            enemy.TakeDamage(9999);

            // Destroy the bullet
            Destroy(gameObject);
        }
    }
}
