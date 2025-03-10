using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    public GameObject groundSlamPowerupPrefab;
    
    private float spawnRangeX = 10;
    private float spawnZMin = 15;
    private float spawnZMax = 25;

    public int enemyCount;
    public int waveCount = 1;
    public int maxWaves = 5;

    public float enemySpeed = 250;
    private float speedIncrement = 20;
    private int baseEnemiesPerWave = 3;
    
    private bool gameOver = false;

    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI youWonText;
    public TextMeshProUGUI finalScoreText;

    private int winScore = 5;
    public GameObject player;
    public ScoreManager scoreManager;

    void Start()
    {
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        
        if (MenuManager.level > 1) 
        {
            int difficultyFactor = MenuManager.level;
            enemySpeed += speedIncrement * difficultyFactor;
        }
    }

    void Update()
    {
        if (gameOver)
        {
            return;
        }

        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (enemyCount == 0 && waveCount <= maxWaves)
        {
            SpawnEnemyWave(waveCount);
        }
        else if (waveCount > maxWaves)
        {
            CheckWinCondition();
        }
    }

    Vector3 GenerateSpawnPosition()
    {
        float xPos = Random.Range(-spawnRangeX, spawnRangeX);
        float zPos = Random.Range(spawnZMin, spawnZMax);
        return new Vector3(xPos, 0, zPos);
    }

    void SpawnEnemyWave(int waveMultiplier)
    {
        if (gameOver) return;

        Vector3 powerupSpawnOffset = new Vector3(0, 0, -15);

        if (GameObject.FindGameObjectsWithTag("PowerUp").Length == 0)
        {
            Instantiate(powerupPrefab, GenerateSpawnPosition() + powerupSpawnOffset, powerupPrefab.transform.rotation);
        }

        PlayerControllerX playerController = player.GetComponent<PlayerControllerX>();

        if (GameObject.FindGameObjectsWithTag("GroundSlamPowerup").Length == 0 && !playerController.hasGroundSlamPowerup)
        {
            Instantiate(groundSlamPowerupPrefab, GenerateSpawnPosition() + powerupSpawnOffset, groundSlamPowerupPrefab.transform.rotation);
        }

        int enemiesToSpawn = baseEnemiesPerWave + waveMultiplier;

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
            enemy.GetComponent<EnemyX>().SetSpeed(enemySpeed);
        }

        waveCount++;
        enemySpeed += speedIncrement;
        ResetPlayerPosition();
    }

    public void ResetPlayerPosition()
    {
        player.transform.position = new Vector3(0, 1, -2);
        player.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    public void CheckWinCondition()
    {
        if (scoreManager.playerScore >= winScore)
        {
            Debug.Log("Player Wins! 🎉");
            EndGame();
        }
        else if (scoreManager.enemyScore >= winScore)
        {
            Debug.Log("Game Over! Enemy Wins 💀");
            EndGame();
        }
    }

    void EndGame()
    {
        gameOver = true;
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
        finalScoreText.text = "Final Score: " + scoreManager.playerScore + " - " + scoreManager.enemyScore;

        if (scoreManager.playerScore >= winScore)
        {
            youWonText.gameObject.SetActive(true);
            gameOverText.gameObject.SetActive(false);
        }
        else
        {
            gameOverText.gameObject.SetActive(true);
            youWonText.gameObject.SetActive(false);
        }
    }
}