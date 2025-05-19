using UnityEngine;
using UnityEngine.UI;

public class PlayerCannon : MonoBehaviour
{
    public ParticleSystem fireParticle;
    public Animator CannonAnimator;

    public GameObject bulletPrefab; // G�lle prefab�
    public Transform firePoint;     // G�lle ��k�� noktas�
    public float bulletSpeed = 10f; // G�lle h�z�
    public Button fireButton;
    public Slider slider;
    public float fillSpeed = 0.5f;  // Slider'ın dolma hızı (saniyede ne kadar artsın)
    private bool isFilling = true;


    void Start()
    {
        slider.value = 0f;
        fireButton.interactable = false;
    }

    void Update()
    {
        if (isFilling)
        {
            slider.value += fillSpeed * Time.deltaTime;
            if (slider.value >= 1f)
            {
                slider.value = 1f;
                fireButton.interactable = true;
                isFilling = false;
            }
        }
    }
    public void Fire()
    {
        fireParticle.Play();
        CannonAnimator.SetTrigger("MainShipFire");
        slider.value = 0f;
        isFilling = true;
        fireButton.interactable = false;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); // Gülle oluştur

        GameObject targetEnemy = FindClosestEnemy();
        if (targetEnemy != null)
        {
            bullet.GetComponent<CannonBall>().SetTarget(targetEnemy.transform);
        }
    }

    GameObject FindClosestEnemy() //En yakın düşmanı bul
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                closest = enemy;
            }
        }
        return closest;
    }
}
