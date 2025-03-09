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
        RestrictPosition();
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
        Vector3 velocity = enemyRb.linearVelocity; // Get current velocity
        float dampingFactor = 0.8f; // Adjust this to control energy loss (0.8 = 80% of original speed)

        if (transform.position.x < -18.5f)
        {
            transform.position = new Vector3(-18f, transform.position.y, transform.position.z);
            velocity.x = -velocity.x * dampingFactor; // Reverse and reduce velocity
        }
        if (transform.position.x > 18.5f)
        {
            transform.position = new Vector3(18f, transform.position.y, transform.position.z);
            velocity.x = -velocity.x * dampingFactor;
        }
        if (transform.position.z < -9.5f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -9f);
            velocity.z = -velocity.z * dampingFactor;
        }
        if (transform.position.z > 29.5f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 29f);
            velocity.z = -velocity.z * dampingFactor;
        }

        enemyRb.linearVelocity = velocity; // Apply the modified velocity
    }
}
