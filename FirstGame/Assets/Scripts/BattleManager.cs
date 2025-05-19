using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;
    public Transform spawnArea; // Düþmanlarýn doðacaðý alan
    

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void PrepareBattle(GameObject[] enemyPrefabs, int[] enemyCounts)
    {
        // Önce sahnedeki eski düþmanlarý temizle
        foreach (Transform child in spawnArea)
        {
            Destroy(child.gameObject);
        }

        // Sonra yenilerini spawnla
        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            for (int j = 0; j < enemyCounts[i]; j++)
            {
                float xPos = (i % 2 == 0) ? j * 2f : j * 2f + 1f;
                Vector3 pos = spawnArea.position + new Vector3(xPos, 0, i * 2f);

                // Prefab'ýn kendi rotasyonunu al ve onunla instantiate et
                Quaternion enemyRotation = enemyPrefabs[i].transform.rotation;

                Instantiate(enemyPrefabs[i], pos, enemyRotation, spawnArea);
            }
        }

    }
}
