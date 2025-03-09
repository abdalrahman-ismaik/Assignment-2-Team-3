﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;             // Existing powerup prefab
    public GameObject groundSlamPowerupPrefab;     // New ground slam powerup prefab

    private float spawnRangeX = 10;
    private float spawnZMin = 15; // set min spawn Z
    private float spawnZMax = 25; // set max spawn Z

    public int enemyCount;
    public int waveCount = 1;
    public int maxWaves = 5;

    public float enemySpeed = 250; // Initial enemy speed
    private float speedIncrement = 20; // Speed increase per wave

    public GameObject player;
    public ScoreManager scoreManager;

    private bool gameOver = false; //added to prevent extra waves

    void Start()
    {
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            return;
        }
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        //if the number of waves is less than 5, then continue to spawn
        if (enemyCount == 0 && waveCount <= maxWaves)
        {
            SpawnEnemyWave(waveCount);
        }
        else if (waveCount > maxWaves)
        {
            CheckWinCondition();
        }
    }

    // Generate random spawn position for powerups and enemy balls
    Vector3 GenerateSpawnPosition()
    {
        float xPos = Random.Range(-spawnRangeX, spawnRangeX);
        float zPos = Random.Range(spawnZMin, spawnZMax);
        return new Vector3(xPos, 0, zPos);
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        if (gameOver) return;

        //Stop spawning if max waves reached


        Vector3 powerupSpawnOffset = new Vector3(0, 0, -15); // make powerups spawn at player end

        // Spawn the existing regular powerup if none exists.
        if (GameObject.FindGameObjectsWithTag("PowerUp").Length == 0)
        {
            Instantiate(powerupPrefab, GenerateSpawnPosition() + powerupSpawnOffset, powerupPrefab.transform.rotation);
        }

        // Get the player's controller component to check if they already have the ground slam powerup.
        PlayerControllerX playerController = player.GetComponent<PlayerControllerX>();

        // Spawn the ground slam powerup if none exists and the player hasn't picked it up.
        if (GameObject.FindGameObjectsWithTag("GroundSlamPowerup").Length == 0 && !playerController.hasGroundSlamPowerup)
        {
            Instantiate(groundSlamPowerupPrefab, GenerateSpawnPosition() + powerupSpawnOffset, groundSlamPowerupPrefab.transform.rotation);
        }

        // Spawn enemy balls based on the current wave number.
        for (int i = 0; i < waveCount; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
            enemy.GetComponent<EnemyX>().SetSpeed(enemySpeed);
        }

        waveCount++;
        enemySpeed += speedIncrement;
        ResetPlayerPosition(); // Reset player position after wave spawn.
    }

    // Move player back to starting position.
    void ResetPlayerPosition()
    {
        player.transform.position = new Vector3(0, 1, -2);
        player.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    public void CheckWinCondition()
    {
        if (scoreManager.playerScore >= 5)
        {
            Debug.Log("Player Wins! 🎉");
            EndGame();
            // Load win scene or show message
        }
        else if (scoreManager.enemyScore >= 5)
        {
            Debug.Log("Game Over! Enemy Wins 💀");
            EndGame();
            // Load game over scene or show message
        }
    }

    void EndGame()
    {
        gameOver = true; // 🔴 Stop spawning
    }
}
