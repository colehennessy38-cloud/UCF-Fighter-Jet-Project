using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    private float speed = 2.5f;
    private float zigzagSpeed = 3f;
    private float amplitude = 1.5f;

    private float startX;

    private GameManager gameManager;

    void Start()
    {
        startX = transform.position.x;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        // Move down
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // Zigzag left and right
        float xOffset = Mathf.Sin(Time.time * zigzagSpeed) * amplitude;
        transform.position = new Vector3(startX + xOffset, transform.position.y, 0);

        // Destroy when off screen
        if (transform.position.y < -6.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if (whatDidIHit.CompareTag("Player"))
        {
            whatDidIHit.GetComponent<PlayerController>().LoseALife();
            Destroy(gameObject);
        }
        else if (whatDidIHit.CompareTag("Weapons"))
        {
            Destroy(whatDidIHit.gameObject);

            gameManager.AddScore(2);

            Destroy(gameObject);
        }
    }
}