using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    private Rigidbody enemyRb;
    private GameObject player;

// check check 1 2 3
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();        
        player = GameObject.Find("Player"); 
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);  

        if(transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
