using System;
using UnityEngine;

public class LevelPoint : MonoBehaviour
{
    public int levelID;
    public Camera cam2, cam3;
    public GameObject Panels, title, pause;
    public GameObject ShipHealthBar;

    public GameObject[] enemyPrefabs; // Her level için farklý düþmanlar
    public int[] enemyCounts;         // Her prefab için kaç tane spawnlansýn

    private void Start()
    {
        cam3.enabled = false;   
    }
    void OnMouseDown()
    {
        Debug.Log("Level " + levelID + " seçildi.");
        cam2.enabled = false;
        cam3.enabled = true;
        Panels.SetActive(false);
        title.SetActive(false);
        pause.SetActive(true);
        ShipHealthBar.SetActive(true);
        BattleManager.Instance.PrepareBattle(enemyPrefabs, enemyCounts);

    }
}
