using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class CannonContent : MonoBehaviour
{
    public ScrollRect scrollRect;
    public Transform content;

    public Image[] materialIcons; // 6 adet malzeme ikonu
    public TextMeshProUGUI[] materialTexts;  // 6 adet malzeme miktarý

    public List<BallData> ballList;

    public Button produceButton; // Üretim butonu
    public TextMeshProUGUI warningText; // Uyarý mesajý

    private int currentIndex = 0;
    private int totalBalls;
    private bool isLerping = false;

    private Dictionary<Sprite, int> playerInventory = new Dictionary<Sprite, int>(); // Oyuncu envanteri

    void Start()
    {
        totalBalls = content.childCount;
        produceButton.onClick.AddListener(ProduceBall);
        InitializeInventory(); // Baþlangýçta envanter ekleyelim
        UpdateMaterials(0);
    }

    void Update()
    {
        if (!isLerping)
        {
            float normalizedPosition = scrollRect.horizontalNormalizedPosition;
            int newIndex = Mathf.RoundToInt(normalizedPosition * (totalBalls - 1));

            if (newIndex != currentIndex)
            {
                StartCoroutine(SmoothScroll(newIndex));
                UpdateMaterials(newIndex);
            }
        }
    }

    IEnumerator SmoothScroll(int targetIndex)
    {
        isLerping = true;
        currentIndex = targetIndex;

        float targetPosition = (float)currentIndex / (totalBalls - 1);
        float duration = 0.2f;
        float elapsedTime = 0;
        float startPosition = scrollRect.horizontalNormalizedPosition;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            scrollRect.horizontalNormalizedPosition = Mathf.Lerp(startPosition, targetPosition, elapsedTime / duration);
            yield return null;
        }

        scrollRect.horizontalNormalizedPosition = targetPosition;
        isLerping = false;
    }

    void UpdateMaterials(int ballIndex)
    {
        BallData ball = ballList[ballIndex];

        for (int i = 0; i < materialIcons.Length; i++)
        {
            if (i < ball.materials.Count)
            {
                materialIcons[i].sprite = ball.materials[i].icon;
                int playerAmount = playerInventory.ContainsKey(ball.materials[i].icon) ? playerInventory[ball.materials[i].icon] : 0;
                materialTexts[i].text = $"{playerAmount}/{ball.materials[i].amount}"; // Stok gösterimi
                materialIcons[i].gameObject.SetActive(true);
                materialTexts[i].gameObject.SetActive(true);
            }
            else
            {
                materialIcons[i].gameObject.SetActive(false);
                materialTexts[i].gameObject.SetActive(false);
            }
        }
    }

    void ProduceBall()
    {
        BallData selectedBall = ballList[currentIndex];
        bool canProduce = true;

        foreach (var material in selectedBall.materials)
        {
            if (!playerInventory.ContainsKey(material.icon) || playerInventory[material.icon] < material.amount)
            {
                canProduce = false;
                break;
            }
        }

        if (canProduce)
        {
            foreach (var material in selectedBall.materials)
            {
                playerInventory[material.icon] -= material.amount;
            }

            Debug.Log(selectedBall.name + " üretildi!");
            warningText.text = "Üretim baþarýlý!";
            warningText.color = Color.green;
            UpdateMaterials(currentIndex);
        }
        else
        {
            Debug.Log("Malzeme yetersiz!");
            warningText.text = "Malzeme yetersiz!";
            warningText.color = Color.red;
        }
    }

    void InitializeInventory()
    {
        // Örnek malzeme ekleme
        foreach (var ball in ballList)
        {
            foreach (var material in ball.materials)
            {
                if (!playerInventory.ContainsKey(material.icon))
                {
                    playerInventory.Add(material.icon, Random.Range(1, 10)); // Rastgele 1-10 malzeme ver
                }
            }
        }
    }
    public void ResetScrollPosition()
    {
        scrollRect.horizontalNormalizedPosition = 0;
        currentIndex = 0;
        UpdateMaterials(0); // Ýlk topun malzemelerini tekrar yükle
    }

}

[System.Serializable]
public class BallData
{
    public string name;
    public List<MaterialRequirement> materials;
}

[System.Serializable]
public class MaterialRequirement
{
    public Sprite icon;
    public int amount;
}
