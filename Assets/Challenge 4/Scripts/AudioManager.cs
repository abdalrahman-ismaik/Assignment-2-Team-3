using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip playerScoreClip;
    public AudioClip enemyScoreClip;
    public AudioClip MainMenuMusic;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

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

    public void PlayMainMenuMusic()
    {
        audioSource.clip = MainMenuMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = Mathf.Clamp01(volume); // Ensure volume is between 0 and 1
    }
}
