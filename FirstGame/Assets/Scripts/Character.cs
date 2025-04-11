using UnityEngine;
using System.Collections;
using TMPro.Examples;
using Unity.VisualScripting;
//using System;
public class Character : MonoBehaviour
{
    public MainControl TopuArtir;
    public float sideSpeed = 5f;
    private float gecikme;
    private bool moveRight = false;
    private bool moveLeft = false;
    public GameObject[] walls;
    public GameObject[] forests;
    public GameObject[] balls;
    public GameObject[] humans;

    public Transform obje1; // Döndürülecek ilk obje
    public Transform obje2; // Döndürülecek ikinci obje
    void FixedUpdate()
    {
        if (moveRight)
        {
            MoveCharacter(Vector3.forward);
            obje1.rotation = Quaternion.Euler(0, 30, 0);
            obje2.rotation = Quaternion.Euler(0, 30, 0);
        }
        else if (moveLeft)
        {
            MoveCharacter(Vector3.back);
            obje1.rotation = Quaternion.Euler(0, -30, 0);
            obje2.rotation = Quaternion.Euler(0, -30, 0);
        }
        else
        {
            obje1.rotation = Quaternion.Euler(0, 0, 0);
            obje2.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private void MoveCharacter(Vector3 direction)
    {
        float newXPosition = transform.position.z + direction.z * sideSpeed * MainControl.y * Time.deltaTime;
        newXPosition = Mathf.Clamp(newXPosition, -5, 5);
        transform.position = new Vector3(transform.position.x,transform.position.y, newXPosition  );
    }
    public void OnRightButtonDown() => moveRight = true;
    public void OnRightButtonUp() => moveRight = false;
    public void OnLeftButtonDown() => moveLeft = true;
    public void OnLeftButtonUp() => moveLeft = false;

    public void SpawnObject()
    {
        StartCoroutine(SpawnWalls());
        StartCoroutine(SpawnTrees());
        //StartCoroutine(SpawnHuman());
        StartCoroutine(SpawnBalls());
    }
    private IEnumerator SpawnWalls()
    {
        while (true)
        {
            Vector3 spawnPosition = new Vector3(-230, 4.5f, 2.8f);
            int randomTrapIndex = Random.Range(0, walls.Length); // Rasgele tuzak seçimi
            Instantiate(walls[randomTrapIndex], spawnPosition, Quaternion.identity);
            Vector3 spawnPosition2 = new Vector3(-230, 4.5f, -2.8f);
            int randomTrapIndex2 = Random.Range(0, walls.Length); // Rasgele tuzak seçimi
            Instantiate(walls[randomTrapIndex2], spawnPosition2, Quaternion.identity);
            //float spawnInterval = Random.Range(2f, 4f);     // Spawnlama aralýðýný belirle
            yield return new WaitForSeconds(4 * MainControl.x);     // Verilen süre kadar bekle
        } 
    }
    private IEnumerator SpawnTrees()
    {
        while (true)
        { 
            float zPosition = Random.Range(0, 2) == 0 ? 13f : -11f;
            Vector3 spawnPosition2 = new Vector3(-230, 2.4f, zPosition);
            int randomForestsIndex = Random.Range(0, forests.Length);
            Instantiate(forests[randomForestsIndex], spawnPosition2, Quaternion.identity);
            float spawnInterval2 = Random.Range(1f, 2f);
            yield return new WaitForSeconds(spawnInterval2 * MainControl.x);
        }
    }
    private IEnumerator SpawnBalls()
    {
        while (true)
        {
            yield return new WaitForSeconds(2 * MainControl.x);
            Vector3 spawnPosition = new Vector3(-230, 2.5f, Random.Range(-2.5f, 2.5f));
            int randomTrapIndex = Random.Range(0, balls.Length); // Rasgele tuzak seçimi
            Instantiate(balls[randomTrapIndex], spawnPosition, Quaternion.identity);
            //float spawnInterval = Random.Range(2f, 4f);     // Spawnlama aralýðýný belirle
            yield return new WaitForSeconds(2 * MainControl.x);

        }
    }
    private IEnumerator SpawnHuman()
    {
        while (true)
        {
            Vector3 spawnPosition = new Vector3(-230, 2.5f, Random.Range(-2.5f, 2.5f));
            int randomHumanIndex = Random.Range(0, humans.Length);
            Instantiate(humans[randomHumanIndex], spawnPosition, Quaternion.identity);
            float spawnInterval3 = Random.Range(2f, 3f);
            yield return new WaitForSeconds(spawnInterval3 * MainControl.x);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BallPlus"))
        {
            TopuArtir.TopSayisiArtir();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Wall"))
        {
            Time.timeScale = 0.0f;
            TopuArtir.GameOverPanelOpen();
        }
    }
}
