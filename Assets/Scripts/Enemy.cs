using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Explosion effect when enemy is destroyed
    public GameObject explosionPrefab;

    // Reference to GameManager for score tracking
    private GameManager gameManager;

    void Start()
    {
        
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        // If enemy collides with the player
        if (whatDidIHit.tag == "Player")
        {
            // Damage player
            whatDidIHit.GetComponent<PlayerController>().LoseALife();

            // Play explosion effect at enemy position
            GameObject boom = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(boom, 1f);

           
            Destroy(this.gameObject);
        }

        // If enemy is hit by a bullet
        else if (whatDidIHit.tag == "Weapons")
        {
            // Remove bullet after contact
            Destroy(whatDidIHit.gameObject);

            // Play explosion 
            GameObject boom = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(boom, 1f);

            // Add score for killing enemy
            gameManager.AddScore(1);

            // Destroy enemy
            Destroy(this.gameObject);
        }
    }
}