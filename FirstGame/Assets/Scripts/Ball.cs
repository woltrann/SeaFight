using UnityEngine;

public class Ball : MonoBehaviour
{
    public ParticleSystem fire;

    private void Start()
    {
        fire.Play();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
