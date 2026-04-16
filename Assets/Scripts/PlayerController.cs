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

        if (shieldActive)
        {
            shieldActive = false;
            shieldPrefab.SetActive(false);
            

        }
        gameManager.ChangeLivesText(lives);
        if (lives < 1)
        {
            // explosions now disappears after 1 second
            GameObject boom = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(boom, 1f);
            gameManager.GameOver();

            Destroy(this.gameObject);
        }
    }

    IEnumerator ShieldPowerDown()
    {
        yield return new WaitForSeconds(5);
        shieldPrefab.SetActive(false);
        shieldActive = false;
        gameManager.PlaySound(2);
        gameManager.ManagePowerupText(5);
    }
    IEnumerator SpeedPowerDown()
    {
        yield return new WaitForSeconds(5);
        playerSpeed = 5f;
        thrusterPrefab.SetActive(false);
        thrusterActive = false;
        gameManager.PlaySound(2);
        gameManager.ManagePowerupText(5);
    }
    IEnumerator WeaponPowerDown()
    {
        yield return new WaitForSeconds(5);
        weaponType = 1;
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
                    thrusterPrefab.SetActive(true);
                    playerSpeed = 10f;
                    gameManager.ManagePowerupText(1);
                    StartCoroutine(SpeedPowerDown());
                    break;
                case 2:
                    gameManager.ManagePowerupText(2);
                    break;
                case 3:
                    gameManager.ManagePowerupText(3);
                    break;
                case 4:
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

            if (transform.position.x > horizontalScreenSize || transform.position.x <= -horizontalScreenSize)
            {
                transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
            }

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
                Instantiate(bulletPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            }
        }

    

}
