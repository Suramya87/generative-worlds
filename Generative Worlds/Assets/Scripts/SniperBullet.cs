using UnityEngine;

public class SniperBullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 3f;
    public int damage = 10;

    private void OnEnable()
    {
        CancelInvoke();
        Invoke(nameof(Deactivate), lifetime);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance?.PauseGame();
            gameObject.SetActive(false);
        }
    }

}
