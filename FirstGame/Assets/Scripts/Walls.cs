using TMPro;
using UnityEngine;
public enum MalzemeTuru { Cam, Tahta, Tugla, Tas, Demir, Celik }
public class Walls : MonoBehaviour
{
    public MalzemeTuru malzemeTuru; // Duvarýn vereceði malzeme türü
    public int malzemeMiktari = 3; // Duvarýn vereceði malzeme miktarý

    public MainControl Point;
    public ParticleSystem crash;
    public int wallHealth; // Kaç top çarpýnca yýkýlacaðýný belirler
    public int puan ;
    public static int DuvarSayisi=0;
    public TextMeshProUGUI healthText;

    void Start()
    {
        Point = GameObject.Find("MainControl").GetComponent<MainControl>();
        healthText.text = "x" + wallHealth.ToString();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (crash != null) crash.Play();

            wallHealth--; // Saðlýðý 1 azalt
            healthText.text = "x"+wallHealth.ToString();

            if (wallHealth <= 0)
            {
                DuvarSayisi++;
                healthText.text = " ";
                Point.SkorArtir(puan);
                Point.MalzemeVer(malzemeTuru, malzemeMiktari);
                Destroy(gameObject);
            }
        }
    }
}
