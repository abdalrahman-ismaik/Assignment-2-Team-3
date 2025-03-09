using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    private AudioSource audioSource;
    public AudioClip backgroundMusicClip;
    public AudioClip playerScoreClip;
    public AudioClip enemyScoreClip;

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
            return;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (backgroundMusicClip != null)
        {
            audioSource.clip = backgroundMusicClip;
            audioSource.loop = true;
            audioSource.Play();
        }

    }

    // Method to play the player's goal sound (crowd clapping)
    public void PlayPlayerScoreSound()
    {
        audioSource.PlayOneShot(playerScoreClip);  // Play the player score sound
    }

    // Method to play the enemy's goal sound (crowd "Awww")
    public void PlayEnemyScoreSound()
    {
        audioSource.PlayOneShot(enemyScoreClip);  // Play the enemy score sound
    }

}
