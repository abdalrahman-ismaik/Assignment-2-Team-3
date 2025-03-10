using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // Required for coroutines

public class ScoreManager : MonoBehaviour
{
    public int playerScore = 0;
    public int enemyScore = 0;
    public int winningScore = 5; // Adjust as needed

    public AudioManager audioManager;
    public SpawnManagerX spawnManager; // Reference to SpawnManager

    void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManagerX>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void IncreasePlayerScore()
    {
        playerScore++;
        audioManager.PlayPlayerScoreSound();
        spawnManager.CheckWinCondition();
        CheckWinCondition();
    }

    public void IncreaseEnemyScore()
    {
        enemyScore++;
        audioManager.PlayEnemyScoreSound();
        spawnManager.CheckWinCondition();
        CheckWinCondition();
    }

    void CheckWinCondition()
    {
        if (playerScore >= winningScore)
        {
            StartCoroutine(LoadNextSceneWithDelay(5f)); // 5-second delay before loading the next scene
        }
        else if (enemyScore >= winningScore)
        {
            LoadMainMenu();
        }
    }

    IEnumerator LoadNextSceneWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        LoadNextScene();
    }

    void LoadNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more levels. Returning to main menu.");
            LoadMainMenu();
        }
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu"); // Ensure "Main Menu" is correctly named in Build Settings
    }
}
