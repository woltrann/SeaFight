using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public MalzemeTuru malzemeTuru; // Duvarın vereceği malzeme türü
    public int malzemeMiktari = 3; // Duvarın vereceği malzeme miktarı
    private MainControl Point;
    private Transform playerShip;
    public Transform firePoint;     // G�lle ��k�� noktas�
    public Slider healthSlider; // World space slider (can barı)
    public GameObject bulletPrefab; // G�lle prefab�
    public float maxHealth = 100f;
    private float currentHealth;

    public Animator EnemyWreckedAnimator;


    void Start()
    {
        Point = GameObject.Find("MainControl").GetComponent<MainControl>();
        playerShip = GameObject.FindGameObjectWithTag("Player").transform;

        currentHealth = maxHealth;
        UpdateHealthBar();
        InvokeRepeating("EnemyFire", 2f, 2f);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth / maxHealth;
        }
    }

    void Die()
    {
        EnemyWreckedAnimator.SetTrigger("Wrecked");
        Point.MalzemeVer(malzemeTuru, malzemeMiktari);
    }
    public void EnemyFire()
    {
        if (playerShip == null) return;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity); // Gülle oluştur
        Vector3 direction = (playerShip.position - firePoint.position).normalized;
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = direction * 10;
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
