using UnityEngine;

public class HealthPowerup : MonoBehaviour
{
    public float lifetime = 4f;   // stays on screen for a few seconds
    public float moveSpeed = 0f;

    void Start()
    {
        Destroy(gameObject, lifetime); // auto destroy if not collected
    }

    void Update()
    {
        
        if (moveSpeed != 0)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player.lives < 3)
            {
                // Heal player
                player.lives++;
                player.gameManager.ChangeLivesText(player.lives);
            }
            else
            {
                //full health gives score
                player.gameManager.AddScore(1);
            }

            Destroy(gameObject);
        }
    }
}

