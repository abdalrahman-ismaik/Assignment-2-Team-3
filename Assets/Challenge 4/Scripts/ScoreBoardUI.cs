using UnityEngine;
using TMPro;

public class ScoreBoardUI : MonoBehaviour
{
    public TextMeshProUGUI scoreTextPlayer; // Reference to the TextMeshPro object that displays the player's score
    public TextMeshProUGUI scoreTextEnemy;  // Reference to the TextMeshPro object that displays the enemy's score
    public ScoreManager scoreManager; // Reference to the ScoreManager that holds player and enemy scores

    void Update()
    {
        // Check if the references are assigned
        if (scoreManager != null && scoreTextPlayer != null && scoreTextEnemy != null)
        {
            // Update player score text
            scoreTextPlayer.text = "Player: " + scoreManager.playerScore.ToString();

            // Update enemy score text
            scoreTextEnemy.text = "Enemy: " + scoreManager.enemyScore.ToString();
        }
        else
        {
            Debug.LogError("ScoreManager or ScoreText (Player/Enemy) is not assigned!");
        }
    }
}
