using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip playerScoreClip;
    public AudioClip enemyScoreClip;
    public AudioClip MainMenuMusic;

    void Awake()
    {
        // Initialize audioSource in Awake instead of Start
        audioSource = GetComponent<AudioSource>();
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

    public void SetVolume(float volume)
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
            
        if (audioSource != null)
            audioSource.volume = Mathf.Clamp01(volume);
    }
}