using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed = 1000f;
    private GameObject focalPoint;

    [Header("Regular Powerup Settings")]
    public bool hasPowerup;
    public GameObject powerupIndicator;
    public int powerUpDuration = 7;
    private float normalStrength = 10f;   // How hard to hit enemy without powerup
    private float powerupStrength = 25f;   // How hard to hit enemy with powerup
    public ParticleSystem speedBoostParticle;

    [Header("Ground Slam Powerup Settings")]
    public bool hasGroundSlamPowerup = false;  // Collected new ground slam powerup?
    private bool isGroundSlamming = false;      // Is the ground slam in progress?
    public GameObject powerupIndicator_slam;    // Indicator for the ground slam powerup
    public float groundSlamUpForce = 5f;        // Upward force when activating ground slam
    public float shockwaveRadius = 50f;         // Radius of the shockwave effect on landing
    public float shockwaveForce = 50f;          // Maximum force applied to an enemy in the shockwave

    [Header("Slam Down Settings")]
    public float slamDownForce = 20f;           // Downward force when second press is triggered
    private bool hasSlamDownBeenTriggered = false;  // Ensures the slam-down is only triggered once

    private ScoreManager scoreManager;  // Reference to the ScoreManager
    private SpawnManagerX spawnManager;  // Reference to the SpawnManager

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>(); // Ensure ScoreManager exists in scene
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManagerX>(); // Ensure SpawnManager exists in scene
    }

    void Update()
    {
        // Regular movement.
        float verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed * Time.deltaTime);

        // Update regular powerup indicator position.
        if (powerupIndicator != null)
            powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);
        if (powerupIndicator_slam != null)
            powerupIndicator_slam.transform.position = transform.position + new Vector3(0, -0.6f, 0);

        // Speed boost with Spacebar.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce(focalPoint.transform.forward * 10, ForceMode.Impulse);
            speedBoostParticle.Play();
        }

        // Ground Slam Activation:
        // First press: if player has the ground slam powerup and is not currently slamming, activate the ability.
        if (hasGroundSlamPowerup && !isGroundSlamming && Input.GetKeyDown(KeyCode.F))
        {
            isGroundSlamming = true;
            hasGroundSlamPowerup = false; // Consume the powerup.
            hasSlamDownBeenTriggered = false; // Reset the slam-down flag.
            // Launch the player upward.
            playerRb.AddForce(Vector3.up * groundSlamUpForce, ForceMode.Impulse);
            if (powerupIndicator_slam != null)
                powerupIndicator_slam.SetActive(false);
        }
        // Second press while in air: if already ground slamming and F is pressed again (and not already triggered), slam down quickly.
        else if (isGroundSlamming && Input.GetKeyDown(KeyCode.F) && !hasSlamDownBeenTriggered)
        {
            playerRb.AddForce(Vector3.down * slamDownForce, ForceMode.Impulse);
            hasSlamDownBeenTriggered = true;
        }

        // Reset player position if falling out of bounds.
        if (transform.position.y < -10)
            spawnManager.ResetPlayerPosition();
    }

    // When player collides with powerups, store them.
    private void OnTriggerEnter(Collider other)
    {
        // Regular powerup logic.
        if (other.gameObject.CompareTag("PowerUp"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
            if (powerupIndicator != null)
                powerupIndicator.SetActive(true);
            StartCoroutine(PowerupCooldown());
        }
        // Ground slam powerup logic.
        else if (other.gameObject.CompareTag("GroundSlamPowerup"))
        {
            Destroy(other.gameObject);
            hasGroundSlamPowerup = true;
            if (powerupIndicator_slam != null)
                powerupIndicator_slam.SetActive(true);
        }
    }

    // Coroutine for regular powerup duration.
    IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerup = false;
        if (powerupIndicator != null)
            powerupIndicator.SetActive(false);
    }

    // Handle collisions.
    private void OnCollisionEnter(Collision other)
    {
        // Enemy collision logic.
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = other.gameObject.transform.position - transform.position;
            if (hasPowerup)
                enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            else
                enemyRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);
        }

        // Colliding with enemy goal increases player score.
        if (other.gameObject.name == "Enemy Goal")
            scoreManager.IncreasePlayerScore();

        // Ground slam shockwave logic:
        // When ground slamming and colliding with an object tagged "Ground", trigger the shockwave.
        if (isGroundSlamming && other.gameObject.CompareTag("Ground"))
        {
            // Reset velocity for a solid impact.
            playerRb.linearVelocity = Vector3.zero;

            // Create a shockwave by affecting all enemies within the shockwave radius.
            Collider[] colliders = Physics.OverlapSphere(transform.position, shockwaveRadius);
            foreach (Collider col in colliders)
            {
                if (col.CompareTag("Enemy"))
                {
                    Rigidbody enemyRb = col.GetComponent<Rigidbody>();
                    if (enemyRb != null)
                    {
                        float distance = Vector3.Distance(transform.position, col.transform.position);
                        float multiplier = Mathf.Clamp01(1f - (distance / shockwaveRadius));
                        Vector3 pushDirection = (col.transform.position - transform.position).normalized;
                        enemyRb.AddForce(pushDirection * shockwaveForce * multiplier, ForceMode.Impulse);
                    }
                }
            }
            speedBoostParticle.Play();
            // Reset ground slam state.
            isGroundSlamming = false;
        }
    }
}
