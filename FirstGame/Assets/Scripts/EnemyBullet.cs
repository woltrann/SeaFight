using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float damage = 10f;
    public float shrinkSpeed = 0.5f; // saniyede ne kadar küçülsün

    private void Update()
    {
        transform.localScale += Vector3.one * shrinkSpeed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShipHealth playerHealth = other.GetComponent<ShipHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            Destroy(gameObject); // Gülle yok olsun
        }
    }
}
