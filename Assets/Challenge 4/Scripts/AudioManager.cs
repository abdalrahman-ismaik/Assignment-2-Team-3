using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip playerScoreClip;
    public AudioClip enemyScoreClip;
    public AudioClip MainMenuMusic;
    public static AudioManager instance;
    public AudioClip backgroundMusicClip;

    //The Awake function is called on all objects in the Scene before any object's Start function is called.
    void Awake()
    {
        // Ensure only one AudioManager exists across all scenes
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Keep AudioManager alive across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate AudioManager instances
            return; // Prevent running extra setup code for duplicates
        }
        audioSource = GetComponent<AudioSource>();
        SceneManager.sceneLoaded += OnSceneLoaded; // Listen for scene changes

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayMusicForCurrentScene();

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForCurrentScene();
    }

    // Play appropriate music based on the current scene
    void PlayMusicForCurrentScene()
    {
        if (audioSource == null) return;

        AudioClip newClip = null;

        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            newClip = MainMenuMusic;
        }
        else if (SceneManager.GetActiveScene().name == "Level 1")
        {
            newClip = backgroundMusicClip;
        }

        // STOP previous music if the scene changed
        if (audioSource.isPlaying && audioSource.clip != newClip)
        {
            audioSource.Stop();
        }

        // Play new music if not already playing
        if (newClip != null && audioSource.clip != newClip)
        {
            audioSource.clip = newClip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }


    // Method to play the player's goal sound (crowd clapping)
    public void PlayPlayerScoreSound()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
            
        if (audioSource != null && playerScoreClip != null)
            audioSource.PlayOneShot(playerScoreClip);
    }

    // Method to play the enemy's goal sound (crowd "Awww")
    public void PlayEnemyScoreSound()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
            
        if (audioSource != null && enemyScoreClip != null)
            audioSource.PlayOneShot(enemyScoreClip);
    }

    public void SetVolume(float volume)
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
            
        if (audioSource != null)
            audioSource.volume = Mathf.Clamp01(volume);
    }

    public void PlayMainMenuMusic()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
            
        if (audioSource != null && MainMenuMusic != null)
        {
            audioSource.clip = MainMenuMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}