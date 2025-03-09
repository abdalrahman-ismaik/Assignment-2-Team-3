using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    // Stores difficulty level (-1 = Not selected, 0 = Easy, 1 = Medium, 2 = Hard)
    public static int level = -1; 

    [SerializeField] private Button playButton;
    [SerializeField] private Slider volumeSlider;
    public AudioManager audioManager;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Assign UI elements
        volumeSlider = GameObject.Find("Slider").GetComponent<Slider>();
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        audioManager.PlayMainMenuMusic();

        playButton.interactable = false; // Disable Play button initially
    }

    // Set difficulty and unlock Play button
    public void SetDifficulty(int selectedLevel)
    {
        level = selectedLevel;
        playButton.interactable = true; // Enable Play button
    }

    public void StartGame()
    {
        if (level == -1) // Ensure difficulty is selected
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
        audioManager.SetVolume(volumeSlider.value);
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
