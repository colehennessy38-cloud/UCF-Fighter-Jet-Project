using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bulletPrefab;
    private float playerSpeed;
    private float horizontalInput;
    private float verticalInput;
    public int lives;
    public GameManager gameManager;
    public GameObject explosionPrefab;
    public GameObject thruster;
    public GameObject shield;
    public int weaponType;
    public bool shieldActive;

    void Start()
    {
        playerSpeed = 6f;
        lives = 3;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.ChangeLivesText(lives);
        shieldActive = false;
        weaponType = 1;
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
            gameManager.ChangeLivesText(lives);
        }
        if (shieldActive)
        {
            gameManager.ManagePowerupText(5);
            shield.SetActive(false);
            shieldActive = false;
        }
          // updates lives UI

        if (lives == 0)
        {
            // explosions now disappears after 1 second
            GameObject boom = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(boom, 1f);
            gameManager.GameOver();
            Destroy(this.gameObject);
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
        if (transform.position.y * 1.5f <= -verticalScreenSize )
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, 0);
        }
    }

    void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }
    }
    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if(whatDidIHit.tag == "Powerup")
        {
            Destroy(whatDidIHit.gameObject);
            int whichPowerup = Random.Range(1, 5);
            gameManager.PlaySound(1);
            switch (whichPowerup)
            {
                case 1:
                    // speed powerup
                    playerSpeed = 10f;
                    thruster.SetActive(true);
                    gameManager.ManagePowerupText(1);
                    StartCoroutine(SpeedPowerDown());
                    break;
                case 2:
                    // set weapon type 2
                    // weapon powerdown coroutine
                    weaponType = 2;
                    gameManager.ManagePowerupText(2);
                    StartCoroutine(WeaponPowerDown());
                    break;
                case 3:
                    // set shield, turn on bool and gameobject
                    //shield powerdown coroutine
                    shield.SetActive(true);
                    shieldActive = true;
                    gameManager.ManagePowerupText(3);
                    StartCoroutine(ShieldPowerDown());
                    break;
                case 4:
                    // set weapon type 3
                    weaponType = 3;
                    gameManager.ManagePowerupText(4);
                    StartCoroutine(WeaponPowerDown());
                    break;

            }
        }
        if (whatDidIHit.tag == "Coin")
        {
            Destroy(whatDidIHit.gameObject);
            gameManager.PlaySound(3);
            gameManager.AddScore(1);
        }
    }
    IEnumerator ShieldPowerDown()
    {
        yield return new WaitForSeconds(5);
        shield.SetActive(false);
        shieldActive = false;
        gameManager.PlaySound(2);
        gameManager.ManagePowerupText(5);
    }
    IEnumerator SpeedPowerDown()
    {
        yield return new WaitForSeconds(5);
        playerSpeed = 5f;
        thruster.SetActive(false);
        gameManager.PlaySound(2);
        gameManager.ManagePowerupText(5);

    }
    IEnumerator WeaponPowerDown()
    {
        yield return new WaitForSeconds(3f);
        weaponType = 1;
        gameManager.ManagePowerupText(5);
        gameManager.PlaySound(2);
    }
}
