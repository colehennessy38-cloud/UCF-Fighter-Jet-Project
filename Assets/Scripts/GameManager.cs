using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject enemyOnePrefab;
    public GameObject enemyTwoPrefab;
    public GameObject coleEnemyPrefab;
    public float horizontalScreenSize;
    public float verticalScreenSize;
    public GameObject cloudPrefab;
    private bool gameOver;
    public GameObject gameOverMenu;
    public TextMeshProUGUI powerupText;
    public GameObject powerupPrefab;

    public GameObject healthPowerupPrefab;

    public GameObject audioPlayer;
    public AudioClip powerUpSound;
    public AudioClip powerDownSound;

    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;

    public int score;

    void Start()
    {
        horizontalScreenSize = 10f;
        verticalScreenSize = 6.5f;
        gameOver = false;
        powerupText.text = "No power ups yet";

        InvokeRepeating("CreateEnemyOne", 1, 2);
        InvokeRepeating("CreateEnemyTwo", 5f, 3.5f);
        InvokeRepeating("CreateColeEnemy", 5, 3);

        StartCoroutine(SpawnPowerup());
        StartCoroutine(SpawnHealthPowerup());

        CreateSky();

        scoreText.text = "Score: 0";
    }

    void CreateSky()
    {
        for (int i = 0; i < 30; i++)
        {
            Instantiate(cloudPrefab,
                new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize),
                Random.Range(-verticalScreenSize, verticalScreenSize), 0),
                Quaternion.identity);
        }
    }

    void CreatePowerup()
    {
        Instantiate(powerupPrefab,
            new Vector3(Random.Range(-horizontalScreenSize * .8f, horizontalScreenSize * .8f),
            Random.Range(-verticalScreenSize * .8f, verticalScreenSize * .8f), 0),
            Quaternion.identity);
    }

    void CreateEnemyOne()
    {
        Instantiate(enemyOnePrefab,
            new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) * .9f, verticalScreenSize, 0),
            Quaternion.identity);
    }

    void CreateEnemyTwo()
    {
        Instantiate(enemyTwoPrefab,
            new Vector3(Random.Range(-9f, 9f), 6.5f, 0f),
            Quaternion.identity);
    }

    void CreateColeEnemy()
    {
        Instantiate(coleEnemyPrefab, new Vector3(-horizontalScreenSize, Random.Range(0, verticalScreenSize), 0), Quaternion.identity);
    }

    IEnumerator SpawnPowerup()
    {
        float spawnTime = Random.Range(3, 5);
        yield return new WaitForSeconds(spawnTime);
        CreatePowerup();
        StartCoroutine(SpawnPowerup());
    }

    IEnumerator SpawnHealthPowerup()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(8f, 15f));

            Instantiate(
                healthPowerupPrefab,
                new Vector3(
                    Random.Range(-horizontalScreenSize * 0.8f, horizontalScreenSize * 0.8f),
                    Random.Range(-verticalScreenSize * 0.8f, verticalScreenSize * 0.8f),
                    0
                ),
                Quaternion.identity
            );
        }
    }

    public void ManagePowerupText(int powerupType)
    {
        switch (powerupType)
        {
            case 1:
                powerupText.text = "Speed!";
                break;
            case 2:
                powerupText.text = "Double Weapon!";
                break;
            case 3:
                powerupText.text = "Triple Weapon";
                break;
            case 4:
                powerupText.text = "Shield!";
                break;
            case 6:
                powerupText.text = "Health Bonus!";
                break;
            default:
                powerupText.text = "No Powerups Yet!";
                break;
        }
    }

    public void PlaySound(int whichSound)
    {
        switch (whichSound)
        {
            case 1:
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(powerUpSound);
                break;
            case 2:
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(powerDownSound);
                break;
        }
    }

    public void ChangeLivesText(int currentLives)
    {
        livesText.text = "Lives: " + currentLives;
    }

    public void AddScore(int earnedScore)
    {
        score += earnedScore;
        scoreText.text = "Score: " + score;
    }

    public void RemoveScore(int earnedScore)
    {
        score -= earnedScore;
        scoreText.text = "Score " + score;
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        gameOver = true;
    }

    void Update()
    {
        if (gameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
