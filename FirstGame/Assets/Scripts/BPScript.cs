using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class BPScript : MonoBehaviour
{
    public TextMeshProUGUI diamondText;
    public TextMeshProUGUI cardText;
    public int diamondSayisi = 0;
    public int cardSayisi = 0;

    public Button unlockButton; // Butonlar� a�an ana buton
    //public Button exitButton;
    public Button[] battlePassButtonsL; // Sol 30 butonu i�eren dizi
    public Button[] battlePassButtonsR; // sa� 30 butonu i�eren dizi
    private List<Button> lockedLeft = new List<Button>(); // Kilitli sol butonlar listesi
    private List<Button> lockedRight = new List<Button>(); // Kilitli sa� butonlar listesi
    private int unlockCounter = 0; // T�klama sayac�
    private Dictionary<Button, Color> originalColors = new Dictionary<Button, Color>(); // Butonlar�n orijinal renkleri



    void Start()
    {
        foreach (Button btn in battlePassButtonsL)
            LockButton(btn, lockedLeft);
        foreach (Button btn in battlePassButtonsR)
            LockButton(btn, lockedRight);
        unlockButton.onClick.AddListener(UnlockButtons);
    }
    void Update()
    {
        
    }


    //BattlePass butonlar�
    public void ButtonClicked(int buttonIndex)
    {
        int diamondsGained = 0;
        int cardsGained = 0;
        switch (buttonIndex)
        {
            case 0: diamondsGained = 1; break;
            case 1: diamondsGained = 2; break;
            case 2: diamondsGained = 3; break;
            case 3: diamondsGained = 4; break;
            case 4: diamondsGained = 5; break;
            case 5: diamondsGained = 10; break;
            case 6: diamondsGained = 15; break;
            case 7: diamondsGained = 20; break;
            case 8: diamondsGained = 25; break;
            case 9: cardsGained = 1; break;
            case 10: cardsGained = 2; break;
            case 11: cardsGained = 3; break;
            case 12: cardsGained = 5; break;
            default: diamondsGained = 0; break;
        }
        diamondSayisi += diamondsGained;
        diamondText.text = diamondSayisi.ToString();
        cardSayisi += cardsGained;
        cardText.text = cardSayisi.ToString();

    }
    public void DisableCurrentButton()
    {
        Button clickedButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        clickedButton.interactable = false;
        clickedButton.image.color = new Color(0.333f, 0.627f, 0.992f); // #55A0FD
        clickedButton.transform.Find("TickIcon").gameObject.SetActive(true); // Tik i�aretini g�r�n�r yap

    }
    void LockButton(Button btn, List<Button> lockedList)
    {
        btn.interactable = false; // T�klamay� engelle
        originalColors[btn] = btn.colors.normalColor;   // Orijinal rengini sakla
        ColorBlock cb = btn.colors;
        cb.disabledColor = new Color(0.75f, 0.75f, 0.77f); // BFBFC5 rengi
        btn.colors = cb;
        if (btn.image != null)
        {
            btn.image.color = new Color(0.75f, 0.75f, 0.77f); // BFBFC5 rengi
        }
        lockedList.Add(btn); // Butonu kilitli listeye ekle
    }
    void UnlockButtons()
    {
        unlockCounter++; // Her t�klamada sayac� art�r

        if (unlockCounter % 3 == 0) // Her 3 t�klamada bir �ift buton a�
        {
            if (lockedLeft.Count > 0)
                UnlockButton(lockedLeft);

            if (lockedRight.Count > 0)
                UnlockButton(lockedRight);
        }
    }
    void UnlockButton(List<Button> lockedList)
    {
        Button buttonToUnlock = lockedList[0]; // �lk kilitli butonu se�
        lockedList.RemoveAt(0); // Listeden ��kar (a��ld��� i�in art�k kilitli de�il)

        buttonToUnlock.interactable = true; // T�klanabilir yap

        // Orijinal rengine geri d�nd�r
        ColorBlock cb = buttonToUnlock.colors;
        cb.disabledColor = originalColors[buttonToUnlock]; // Normal rengine d�nd�r
        buttonToUnlock.colors = cb;
        if (buttonToUnlock.image != null)
        {
            buttonToUnlock.image.color = new Color(0.0f, 0.44f, 0.99f); // #0071FD Rengi

        }
    }
}
