using System.Collections;
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

    public float enemySpeed = 250; // Initial enemy speed
    private float speedIncrement = 20; // Speed increase per wave

    public GameObject player; 

    // Update is called once per frame
    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (enemyCount == 0)
        {
            SpawnEnemyWave(waveCount);
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
        Vector3 powerupSpawnOffset = new Vector3(0, 0, -15); // make powerups spawn at player end

        // Spawn the existing regular powerup if none exists.
        if (GameObject.FindGameObjectsWithTag("PowerUp").Length == 0)
        {
            Instantiate(powerupPrefab, GenerateSpawnPosition() + powerupSpawnOffset, powerupPrefab.transform.rotation);
        }

        // Spawn the ground slam powerup if none exists.
        if (GameObject.FindGameObjectsWithTag("GroundSlamPowerup").Length == 0)
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
}
