using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public GameObject bulletPrefab;

    private float playerSpeed;
    private float horizontalInput;
    private float verticalInput;

    public int lives;
    public GameManager gameManager;

    public GameObject explosionPrefab;
    public GameObject thrusterPrefab;
    public GameObject shieldPrefab;

    public int weaponType;

    public bool shieldActive;
    public bool thrusterActive;

    void Start()
    {
        shieldActive = false;
        weaponType = 1;

        playerSpeed = 6f;
        lives = 3;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.ChangeLivesText(lives);
    }

    void Update()
    {
        Shooting();
        Movement();
    }

    public void LoseALife()
    {
        if (!shieldActive)
        {
            lives--;
        }
        else
        {
            // Shield absorbs the hit
            shieldActive = false;
            shieldPrefab.SetActive(false);
            gameManager.PlaySound(2);
        }

        gameManager.ChangeLivesText(lives);

        if (lives < 1)
        {
            // explosions disappears after 1 second
            GameObject boom = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(boom, 1f);

            gameManager.GameOver();
            Destroy(this.gameObject);
        }
    }

    IEnumerator ShieldPowerDown()
    {
        yield return new WaitForSeconds(5);
        if (shieldActive)
        {
            shieldPrefab.SetActive(false);
            shieldActive = false;
            gameManager.PlaySound(2);
            gameManager.ManagePowerupText(5);
        }
    }

    IEnumerator SpeedPowerDown()
    {
        yield return new WaitForSeconds(5);
        playerSpeed = 6f;
        thrusterPrefab.SetActive(false);
        thrusterActive = false;
        gameManager.PlaySound(2);
        gameManager.ManagePowerupText(5);
    }

    IEnumerator WeaponPowerDown()
    {
        yield return new WaitForSeconds(5);
        weaponType = 1; // back to single shot
        gameManager.PlaySound(2);
        gameManager.ManagePowerupText(5);
    }

    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if (whatDidIHit.tag == "Powerup")
        {
            Destroy(whatDidIHit.gameObject);

            int whichPowerup = Random.Range(1, 5);
            gameManager.PlaySound(1);

            switch (whichPowerup)
            {
                case 1:
                    // speed boost
                    thrusterPrefab.SetActive(true);
                    playerSpeed = 10f;
                    gameManager.ManagePowerupText(1);
                    StartCoroutine(SpeedPowerDown());
                    break;

                case 2:
                    // doble shot
                    weaponType = 2;
                    gameManager.ManagePowerupText(2);
                    StartCoroutine(WeaponPowerDown());
                    break;

                case 3:
                    // triple shot
                    weaponType = 3;
                    gameManager.ManagePowerupText(3);
                    StartCoroutine(WeaponPowerDown());
                    break;

                case 4:
                    // shields
                    shieldPrefab.SetActive(true);
                    shieldActive = true;
                    gameManager.ManagePowerupText(4);
                    StartCoroutine(ShieldPowerDown());
                    break;
            }
        }
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * playerSpeed);

        float horizontalScreenSize = gameManager.horizontalScreenSize;
        float verticalScreenSize = gameManager.verticalScreenSize;

        // horizontal limits
        if (transform.position.x > horizontalScreenSize || transform.position.x <= -horizontalScreenSize)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }

        // vertical limits
        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, -0.1f, 0);
        }
        if (transform.position.y * 1.5f <= -verticalScreenSize)
        {
            transform.position = new Vector3(transform.position.x, 0.1f, 0);
        }
    }

    void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (weaponType == 1)
            {
                // Single shot
                Instantiate(bulletPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            }
            else if (weaponType == 2)
            {
                // Double shot
                Instantiate(bulletPrefab, transform.position + new Vector3(-0.5f, 1, 0), Quaternion.identity);
                Instantiate(bulletPrefab, transform.position + new Vector3(0.5f, 1, 0), Quaternion.identity);
            }
            else if (weaponType == 3)
            {
                // Triple shot
                Instantiate(bulletPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                Instantiate(bulletPrefab, transform.position + new Vector3(-0.7f, 0.8f, 0), Quaternion.identity);
                Instantiate(bulletPrefab, transform.position + new Vector3(0.7f, 0.8f, 0), Quaternion.identity);
            }
        }
    }
}

