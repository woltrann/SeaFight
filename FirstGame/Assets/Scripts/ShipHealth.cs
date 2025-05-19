using UnityEngine;
using UnityEngine.UI;


public class ShipHealth : MonoBehaviour
{
    public MainControl gameoverPaneli;
    public float maxHealth = 100f;
    private float currentHealth;

    public Slider healthSlider; // Player'ýn world-space can barý

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
        if (currentHealth <= 0) Die();      
    }

    void UpdateHealthBar()
    {
        if (healthSlider != null)   healthSlider.value = currentHealth / maxHealth;   
    }

    void Die()
    {
        Debug.Log("Gemi yok oldu!"); // Ölüm efekti, patlama vs.
        // Destroy(gameObject); // Eðer oyun bitmeli diyorsan bunu aç
        gameoverPaneli.GameOverPanelOpen();
        Time.timeScale = 0f;
    }
}
