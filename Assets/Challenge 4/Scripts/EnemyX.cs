using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyX : MonoBehaviour
{
    private float speed;
    private Rigidbody enemyRb;
    private GameObject playerGoal;
    private ScoreManager scoreManager;  // Reference to the ScoreManager

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        playerGoal = GameObject.Find("Player Goal");
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>(); // Make sure the ScoreManager is in the scene
    }

    // Update is called once per frame
    void Update()
    {
        // Set enemy direction towards player goal and move there
        Vector3 lookDirection = (playerGoal.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed * Time.deltaTime);

        // Restrict the position of the enemy
        
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    private void OnCollisionEnter(Collision other)
    {
        // If enemy collides with either goal, destroy it
        // If enemy collides with Enemy Goal, increase player score
        if (other.gameObject.name == "Enemy Goal")
        {
            scoreManager.IncreasePlayerScore();
            Destroy(gameObject);
        }
        // If enemy collides with Player Goal, increase enemy score
        else if (other.gameObject.name == "Player Goal")
        {
            scoreManager.IncreaseEnemyScore();
            Destroy(gameObject);
        }

        else if(transform.position.y < -10)
        {
            Destroy(gameObject);
        }

    }

    void RestrictPosition()
    {
        // Define the boundary limits
        float minX = -17.69f, maxX = 17.69f;
        float minZ = -9.31f, maxZ = 29.22f;
        float minY = -0.93f, maxY = 4f; // Optional Y limit to prevent falling

        // Clamp the position within the defined boundaries
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedZ = Mathf.Clamp(transform.position.z, minZ, maxZ);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);

        // Apply the clamped position
        transform.position = new Vector3(clampedX, clampedY, clampedZ);
    }
}
