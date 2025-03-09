using UnityEngine;
using UnityEngine.SceneManagement;
public class ScoreManager : MonoBehaviour
{
    public int playerScore = 0;
    public int enemyScore = 0;

    public AudioManager audioManager;
    public SpawnManagerX spawnManager; // Reference to SpawnManager

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManagerX>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Method to increase player score
    public void IncreasePlayerScore()
    {
        playerScore++;
        audioManager.PlayPlayerScoreSound();
    }

    // Method to increase enemy score
    public void IncreaseEnemyScore()
    {
        enemyScore++;
        audioManager.PlayEnemyScoreSound();
    }
}

