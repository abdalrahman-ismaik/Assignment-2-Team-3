using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public int playerScore = 0;
    public int enemyScore = 0;
    public int winningScore = 5; // Adjust as needed

    public AudioManager audioManager;
    public SpawnManagerX spawnManager; // Reference to SpawnManager

    void Start()
    {
        // Find and assign the SpawnManager and AudioManager
        spawnManager = GameObject.Find("Spawn Manager")?.GetComponent<SpawnManagerX>();
        audioManager = GameObject.Find("AudioManager")?.GetComponent<AudioManager>();

        // Log warnings if references are missing
        if (spawnManager == null)
        {
            Debug.LogWarning("SpawnManager not found. Ensure there is a GameObject named 'Spawn Manager' with a SpawnManagerX component.");
        }
        if (audioManager == null)
        {
            Debug.LogWarning("AudioManager not found. Ensure there is a GameObject named 'AudioManager' with an AudioManager component.");
        }
    }

    public void IncreasePlayerScore()
    {
        playerScore++;
<<<<<<< Updated upstream
        audioManager?.PlayPlayerScoreSound(); // Use null conditional operator to avoid errors
=======
        audioManager.PlayPlayerScoreSound();
>>>>>>> Stashed changes
        CheckWinCondition();
    }

    public void IncreaseEnemyScore()
    {
        enemyScore++;
        audioManager?.PlayEnemyScoreSound(); // Use null conditional operator to avoid errors
        CheckWinCondition();
    }

    void CheckWinCondition()
    {
        if (playerScore >= winningScore)
        {
            StartCoroutine(LoadSceneWithDelay(true)); // Player wins, load next scene after delay
        }
        else if (enemyScore >= winningScore)
        {
            StartCoroutine(LoadSceneWithDelay(false)); // Player loses, load main menu after delay
        }
    }

    IEnumerator LoadSceneWithDelay(bool playerWon)
    {
        yield return new WaitForSeconds(7f); // Wait for 7 seconds

        if (playerWon)
        {
            LoadNextScene();
        }
        else
        {
            LoadMainMenu();
        }
    }

    void LoadNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // Check if the next scene index is valid
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("No more levels. Returning to main menu.");
            LoadMainMenu();
        }
    }

    void LoadMainMenu()
    {
        // Ensure the "Main Menu" scene exists in the build settings
        if (SceneUtility.GetBuildIndexByScenePath("Main Menu") >= 0)
        {
            SceneManager.LoadScene("Main Menu");
        }
        else
        {
            Debug.LogError("Main Menu scene not found in build settings. Please add it to the build settings.");
        }
    }
}