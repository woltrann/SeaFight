using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
using TMPro;
public class MainControl : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider backgroundSlider;
    public Slider sfxSlider;
    public Animator cameraAnimator;
    public Animator EnvanterAnimator;
    private Character spawn;
    private TopAtar topAtar;
    public GameObject[] panels;
    public Button[] panelButtons; //panel butonlarý
    public GameObject buttonsPanel;
    private Vector3 defaultScale = new Vector3(1f, 1f, 1f);
    private Vector3 highlightedScale = new Vector3(1.4f, 1.4f, 4f); // Buton büyüklüðü

    public int stokCam, stokTahta, stokTuga, stokTas, stokDemir, stokCelik;
    public int Cam;
    public TextMeshProUGUI CamText;
    public TextMeshProUGUI CamText2;
    public TextMeshProUGUI CamText3;
    public int Tahta;
    public TextMeshProUGUI TahtaText;
    public TextMeshProUGUI TahtaText2;
    public TextMeshProUGUI TahtaText3;
    public int Tugla;
    public TextMeshProUGUI TuglaText;
    public TextMeshProUGUI TuglaText2;
    public TextMeshProUGUI TuglaText3;
    public int Tas;
    public TextMeshProUGUI TasText;
    public TextMeshProUGUI TasText2;
    public TextMeshProUGUI TasText3;
    public int Demir;
    public TextMeshProUGUI DemirText;
    public TextMeshProUGUI DemirText2;
    public TextMeshProUGUI DemirText3;
    public int Celik;
    public TextMeshProUGUI CelikText;
    public TextMeshProUGUI CelikText2;
    public TextMeshProUGUI CelikText3;

    public GameObject mainPanel;
    public GameObject pausePanel;
    public GameObject envanterPanel;
    public GameObject settingsPanel;
    public GameObject highScorePanel;
    public GameObject gameOverPanel;
    public GameObject pauseButton;
    public GameObject ballCount;
    public GameObject LeftButton;
    public GameObject RightButton;
    public Button turkishButton;
    public Button englishButton;
    public Button startButton;
    public TextMeshProUGUI skor;
    public TextMeshProUGUI skorText;
    public TextMeshProUGUI ballText;
    public TextMeshProUGUI YikilanDuvarText;
    public static int ballSayisi = 5;
    public int score = 0;
    public int count = 1;
    public static float x = 1f; 
    public static float y = 1f; 

    void Start()
    {
        spawn = GameObject.Find("Cannon").GetComponent<Character>();
        topAtar = GameObject.Find("TopAtar").GetComponent<TopAtar>();
        Time.timeScale = 1f;
        panels[2].SetActive(true);
        panelButtons[2].transform.localScale = highlightedScale;
        LeftButton.SetActive(false);
        RightButton.SetActive(false);
        pauseButton.SetActive(false);
        skor.gameObject.SetActive(false);
        skorText.gameObject.SetActive(false);
        ballCount.gameObject.SetActive(false);
        buttonsPanel.SetActive(true);
        startButton.onClick.AddListener(StartCameraAnimation);
        EnvanterAnimator = GameObject.Find("EnvanterPanel").GetComponent<Animator>(); 
        backgroundSlider.onValueChanged.AddListener(SetBackgroundVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        turkishButton.onClick.AddListener(() => SetLanguage("tr"));
        englishButton.onClick.AddListener(() => SetLanguage("en"));
    }
    public void ShowPanel(int index)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            bool isActive = (i == index);
            panels[i].SetActive(i == index); // Sadece seçilen panel aktif, diðerleri kapalý
            if (isActive) panelButtons[i].transform.localScale = highlightedScale; // Butonu büyüt
            else panelButtons[i].transform.localScale = defaultScale; // Eski haline getir
     
        }
    }
    void StartCameraAnimation()
    {
        GameObject[] traps = GameObject.FindGameObjectsWithTag("Trap");
        foreach (GameObject trap in traps)
        {
            ObjectMovement trapController = trap.GetComponent<ObjectMovement>();
            trapController.StartGame();
        }
        GameObject[] floors = GameObject.FindGameObjectsWithTag("Floor");
        foreach (GameObject floor in floors)
        {
            GroundLooper floorController = floor.GetComponent<GroundLooper>();
            floorController.FloorMovement();
        }
        ballSayisi=5;
        ballText.text = ballSayisi.ToString();
        spawn.SpawnObject();
        LeftButton.SetActive(true);
        RightButton.SetActive(true);
        pauseButton.SetActive(true);
        skor.gameObject.SetActive(true);
        skorText.gameObject.SetActive(true);
        ballCount.gameObject.SetActive(true);
        skor.text = score.ToString();
        mainPanel.SetActive(false);
        cameraAnimator.SetTrigger("start_trg");
        buttonsPanel.SetActive(false);

    }
    public void SkorArtir(int a)
    {
        score += a;
        skor.text = score.ToString();
        count++;
        if (count >= 5)
        {
            x = Mathf.Max(0.1f, x - 0.1f);
            y += 0.1f;
            count = 1;
            Debug.Log(x + " - " + y);
        }
    }
    public void TopSayisiAzalt()
    {
        ballSayisi -= 1;
        ballText.text = ballSayisi.ToString();
        if (ballSayisi < 0) ballText.text = "0";
    }
    public void TopSayisiArtir()
    {
        ballSayisi += 3;
        ballText.text = ballSayisi.ToString();
    }
    public void PausePanelOpen()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void PausePanelClose()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void EnvanterPanelOC()
    {
        if (!envanterPanel.activeSelf)  EnvanterAnimator.SetTrigger("New"); 
        else  EnvanterAnimator.SetTrigger("New"); 
    }
    public void SettingsPanelOpen() => settingsPanel.SetActive(true);
    public void SettingsPanelClose() => settingsPanel.SetActive(false);
    public void HighScorePanelOpen() => highScorePanel.SetActive(true);
    public void HighScorePanelClose() => highScorePanel.SetActive(false);
    public void GameOverPanelOpen() { YikilanDuvarText.text = "Yýkýlan Duvar Sayýsý: " + Walls.DuvarSayisi; gameOverPanel.SetActive(true); }
    public void SetBackgroundVolume(float volume) => audioMixer.SetFloat("BackgroundVolume", Mathf.Log10(volume) * 20);
    public void SetSFXVolume(float volume) => audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
    public void SetLanguage(string localeCode) => LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale(localeCode);
    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
    public void ExitGame() => Application.Quit();

    public void MalzemeVer(MalzemeTuru malzemeTuru, int malzemeMiktari)
    {
        switch (malzemeTuru)
        {
            case MalzemeTuru.Cam:
                Cam += malzemeMiktari; CamText.text = Cam.ToString(); CamText2.text = Cam.ToString(); CamText3.text = Cam.ToString();
                break;
            case MalzemeTuru.Tahta:
                Tahta += malzemeMiktari; TahtaText.text = Tahta.ToString(); TahtaText2.text = Tahta.ToString(); TahtaText3.text = Tahta.ToString();
                break;
            case MalzemeTuru.Tugla:
                Tugla += malzemeMiktari; TuglaText.text = Tugla.ToString(); TuglaText2.text = Tugla.ToString(); TuglaText3.text = Tugla.ToString();
                break;
            case MalzemeTuru.Tas:
                Tas += malzemeMiktari; TasText.text = Tas.ToString(); TasText2.text = Tas.ToString(); TasText3.text = Tas.ToString();
                break;
            case MalzemeTuru.Demir:
                Demir += malzemeMiktari; DemirText.text = Demir.ToString(); DemirText2.text = Demir.ToString(); DemirText3.text = Demir.ToString();
                break;
            case MalzemeTuru.Celik:
                Celik += malzemeMiktari; CelikText.text = Celik.ToString(); CelikText2.text = Celik.ToString(); CelikText3.text = Celik.ToString();
                break;
        }
    }
}
