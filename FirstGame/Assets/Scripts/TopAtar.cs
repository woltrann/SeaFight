using UnityEngine;

public class TopAtar : MonoBehaviour
{
    public MainControl topuAzalt;
    public GameObject topPrefab; // At�lacak topun prefabi
    public Transform atisNoktasi; // Topun ��kaca�� nokta
    public float atisHizi = 20f; // Topun f�rlatma h�z�

    public ParticleSystem Buff;
    public Animator CannonAnimator;

    void Start()
    {
        //CannonAnimator = GameObject.Find("Canon").GetComponent<Animator>();
    }
    public void TopFirlat()
    {
        if (MainControl.ballSayisi >= 1)
        {  
            topuAzalt.TopSayisiAzalt();
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.Play();
            GameObject yeniTop = Instantiate(topPrefab, atisNoktasi.position, atisNoktasi.rotation);     // Topu oluştur
            Buff.Play();
            CannonAnimator.SetTrigger("CannonFire");
            Rigidbody rb = yeniTop.GetComponent<Rigidbody>();   // Rigidbody ekleyerek ileri doğru hareket ettir
            if (rb != null)   rb.linearVelocity = -atisNoktasi.right * atisHizi;
            
            Destroy(yeniTop, 2f);
        }  
    }
}
