using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    // Stores difficulty level (-1 = Not selected, 0 = Easy, 1 = Medium, 2 = Hard)
    public static int level = -1; 

    // Non-static references to UI elements
    [SerializeField] private Button playButton;
    [SerializeField] private Button easyButton;
    [SerializeField] private Button mediumButton;
    [SerializeField] private Button hardButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioManager audioManager;

    void Awake()
    {
        // Initialize UI
        InitializeUI();
    }

    private void InitializeUI()
    {
        // Find UI elements if not assigned in the inspector
        if (playButton == null)
            playButton = GameObject.Find("PlayButton")?.GetComponent<Button>();
        
        if (easyButton == null)
            easyButton = GameObject.Find("EasyButton")?.GetComponent<Button>();
        
        if (mediumButton == null)
            mediumButton = GameObject.Find("MediumButton")?.GetComponent<Button>();
        
        if (hardButton == null)
            hardButton = GameObject.Find("HardButton")?.GetComponent<Button>();
        
        if (quitButton == null)
            quitButton = GameObject.Find("QuitButton")?.GetComponent<Button>();
        
        if (volumeSlider == null)
            volumeSlider = GameObject.Find("Slider")?.GetComponent<Slider>();
        
        if (audioManager == null)
            audioManager = GameObject.Find("Audio Manager")?.GetComponent<AudioManager>();

        // Set up button listeners
        if (playButton != null)
        {
            playButton.onClick.RemoveAllListeners();
            playButton.onClick.AddListener(StartGame);
            playButton.interactable = level != -1;
        }

        if (easyButton != null)
        {
            easyButton.onClick.RemoveAllListeners();
            easyButton.onClick.AddListener(setEasy);
        }

        if (mediumButton != null)
        {
            mediumButton.onClick.RemoveAllListeners();
            mediumButton.onClick.AddListener(setMedium);
        }

        if (hardButton != null)
        {
            hardButton.onClick.RemoveAllListeners();
            hardButton.onClick.AddListener(setHard);
        }

        if (quitButton != null)
        {
            quitButton.onClick.RemoveAllListeners();
            quitButton.onClick.AddListener(QuitGame);
        }

        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.RemoveAllListeners();
            volumeSlider.onValueChanged.AddListener(delegate { SetVolume(); });
        }
    }

    void Start()
    {
        // If level was set from a previous scene, make sure the UI reflects it
        if (level != -1 && playButton != null)
        {
            playButton.interactable = true;
        }
        
        // Play music after everything is initialized
        if (audioManager != null)
        {
            audioManager.PlayMainMenuMusic();
        }
    }

    // Set difficulty and unlock Play button
    public void SetDifficulty(int selectedLevel)
    {
        level = selectedLevel;
        if (playButton != null)
        {
            playButton.interactable = true;
        }
    }

    public void StartGame()
    {
        if (level == -1)
        {
            Debug.Log("Please select a difficulty before starting the game.");
            return;
        }

        // Load scene based on difficulty selection
        switch (level)
        {
            case 0:
                SceneManager.LoadScene("Level 1");
                break;
            case 1:
                SceneManager.LoadScene("Level 2");
                break;
            case 2:
                SceneManager.LoadScene("Level 3");
                break;
            default:
                Debug.LogError("Invalid difficulty selection.");
                break;
        }
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void SetVolume()
    {
        if (audioManager != null && volumeSlider != null)
        {
            audioManager.SetVolume(volumeSlider.value);
        }
    }

    public void setEasy()
    {
        SetDifficulty(0);
    }

    public void setMedium()
    {
        SetDifficulty(1);
    }

    public void setHard()
    {
        SetDifficulty(2);
    }
}