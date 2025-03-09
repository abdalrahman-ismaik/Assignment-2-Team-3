using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private Rigidbody playerRb;
    private float speed = 500f;
    private GameObject focalPoint;

    public bool hasPowerup;
    public GameObject powerupIndicator;
    public GameObject powerupIndicator_slam; // Indicator for the ground slam powerup
    public int powerUpDuration = 7;
    private float normalStrength = 10f;   // How hard to hit enemy without powerup
    private float powerupStrength = 25f;   // How hard to hit enemy with powerup
    public ParticleSystem speedBoostParticle;

    public bool hasGroundSlamPowerup = false;  // Collected new ground slam powerup?
    private bool isGroundSlamming = false;      // Is the ground slam in progress?
    public float groundSlamUpForce = 5f;        // Upward force when activating ground slam
    public float shockwaveRadius = 50f;           // Radius of the shockwave effect on landing
    public float shockwaveForce = 50f;          // Maximum force applied to an enemy in the shockwave

    private ScoreManager scoreManager;  // Reference to the ScoreManager
    private SpawnManagerX spawnManager;  // Reference to the ScoreManager


    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>(); // Ensure ScoreManager exists in scene
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManagerX>(); // Ensure SpawnManager exists in scene
    }

    void Update()
    {
        // Regular movement
        float verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed * Time.deltaTime);

        // Update the regular powerup indicator position (if active)
        if (powerupIndicator != null)
        {
            powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);
        }
        if (powerupIndicator_slam != null)
        {
            powerupIndicator_slam.transform.position = transform.position + new Vector3(0, -0.6f, 0);
        }

        // Speed boost with Spacebar (existing functionality)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce(focalPoint.transform.forward * 10, ForceMode.Impulse);
            speedBoostParticle.Play();
        }

        // Ground Slam Activation:
        // If player has collected the ground slam powerup and presses F, start the ground slam.
        if (hasGroundSlamPowerup && !isGroundSlamming && Input.GetKeyDown(KeyCode.F))
        {
            isGroundSlamming = true;
            // Consume the ground slam powerup.
            hasGroundSlamPowerup = false;
            // Launch the player upward.
            playerRb.AddForce(Vector3.up * groundSlamUpForce, ForceMode.Impulse);
            
            powerupIndicator_slam.SetActive(false);
        }

        if(transform.position.y < -10)
        {
            spawnManager.ResetPlayerPosition();
        }
    }

    // When player collides with powerups, store them.
    private void OnTriggerEnter(Collider other)
    {
        // Regular powerup logic remains unchanged.
        if (other.gameObject.CompareTag("PowerUp"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
            if (powerupIndicator != null)
            {
                powerupIndicator.SetActive(true);
            }
            StartCoroutine(PowerupCooldown());
        }
        // New ground slam powerup logic.
        else if (other.gameObject.CompareTag("GroundSlamPowerup"))
        {
            Destroy(other.gameObject);
            hasGroundSlamPowerup = true;
            // Optionally, you can also enable the same indicator or a separate one.
            if (powerupIndicator_slam != null)
            {
                powerupIndicator_slam.SetActive(true);
            }
        }
    }

    // Coroutine for regular powerup duration.
    IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerup = false;
        if (powerupIndicator != null)
        {
            powerupIndicator.SetActive(false);
        }
    }

    // Handle collisions.
    private void OnCollisionEnter(Collision other)
    {
        // Existing enemy collision logic.
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = other.gameObject.transform.position - transform.position;

            if (hasPowerup) // Apply extra force if regular powerup is active.
            {
                enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            }
            else // Otherwise, apply normal force.
            {
                enemyRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);
            }
        }

        // Ground slam shockwave logic:
        // When ground slamming and colliding with an object tagged "Ground", trigger the shockwave.
        if (isGroundSlamming && other.gameObject.CompareTag("Ground"))
        {

            // Optionally reset velocity to ensure a solid impact.
            playerRb.linearVelocity = Vector3.zero;

            // Create a shockwave by checking for enemies within the shockwave radius.
            Collider[] colliders = Physics.OverlapSphere(transform.position, shockwaveRadius);
            foreach (Collider col in colliders)
            {
                if (col.CompareTag("Enemy"))
                {
                    Rigidbody enemyRb = col.GetComponent<Rigidbody>();
                    if (enemyRb != null)
                    {
                        // Calculate distance to enemy; closer enemies get a stronger push.
                        float distance = Vector3.Distance(transform.position, col.transform.position);
                        float multiplier = Mathf.Clamp01(1f - (distance / shockwaveRadius));
                        Vector3 pushDirection = (col.transform.position - transform.position).normalized;
                        enemyRb.AddForce(pushDirection * shockwaveForce * multiplier, ForceMode.Impulse);
                    }
                }
            }
            speedBoostParticle.Play();
            // Reset the ground slam state.
            isGroundSlamming = false;
        }
    }
}
