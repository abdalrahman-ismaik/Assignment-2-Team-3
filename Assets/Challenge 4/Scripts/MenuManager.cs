using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    
    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    [SerializeField] private Slider volumeSlider;
    public AudioManager audioManager;

    void Start()
    {
        volumeSlider = GameObject.Find("Slider").GetComponent<Slider>();
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        audioManager.PlayMainMenuMusic();
    }

    public void SetVolume()
    {
        audioManager.SetVolume(volumeSlider.value);
    }

    // public void SetDifficulty(int difficulty)
    // {
    //     PlayerPrefs.SetInt("difficulty", difficulty);
    // }


    
}