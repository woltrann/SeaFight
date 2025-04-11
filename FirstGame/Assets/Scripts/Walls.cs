using TMPro;
using UnityEngine;
public enum MalzemeTuru { Cam, Tahta, Tugla, Tas, Demir, Celik }
public class Walls : MonoBehaviour
{
    public MalzemeTuru malzemeTuru; // Duvar�n verece�i malzeme t�r�
    public int malzemeMiktari = 3; // Duvar�n verece�i malzeme miktar�

    public MainControl Point;
    public ParticleSystem crash;
    public int wallHealth; // Ka� top �arp�nca y�k�laca��n� belirler
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

            wallHealth--; // Sa�l��� 1 azalt
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
