using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public float moveSpeed = 10f;
    private Transform target;
    public float shrinkSpeed = 0.5f; // saniyede ne kadar küçülsün


    public void SetTarget(Transform enemyTarget) => target = enemyTarget;
    
    private void Update()
    {
        if (target == null) {Destroy(gameObject);return;}

        Vector3 direction = (target.position - transform.position).normalized;      // Hedefe doðru hareket et
        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.localScale -= Vector3.one * shrinkSpeed * Time.deltaTime; //Hedefe giderken boyutu küçült
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(40f); // Gülleden gelen hasar
            }
            Destroy(gameObject); // Gülle yok olsun
        }
    }
}
