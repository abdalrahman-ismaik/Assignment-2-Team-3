using UnityEngine;
public class ScoreManager : MonoBehaviour
{
    public int playerScore = 0;
    public int enemyScore = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Method to increase player score
    public void IncreasePlayerScore()
    {
        playerScore++;
    }

    // Method to increase enemy score
    public void IncreaseEnemyScore()
    {
        enemyScore++;
    }
}

