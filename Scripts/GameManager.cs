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
    public GameObject gameOverMenu;
    public GameObject powerupPrefab;
    public GameObject coinPrefab;

    public GameObject audioPlayer;
    public AudioClip powerupSound;
    public AudioClip powerDownSound;
    public AudioClip coinSound;

    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI powerUpText;

    private bool gameOver;

    public int score;

    void Start()
    {
        horizontalScreenSize = 10f;
        verticalScreenSize = 6.5f;

        InvokeRepeating("CreateEnemyOne", 1, 2);
        InvokeRepeating("CreateEnemyTwo", 5f, 3.5f);
        InvokeRepeating("CreateColeEnemy", 5, 3);

        StartCoroutine(SpawnPowerup());
        StartCoroutine(SpawnCoin());

        CreateSky();
        score = 0;
        gameOver = false;

        scoreText.text = "Score: 0";
        powerUpText.text = "No power ups";
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

    void CreatePowerup()
    {
        Instantiate(powerupPrefab, new Vector3(Random.Range(-horizontalScreenSize * 0.8f, horizontalScreenSize * 0.8f), Random.Range(-verticalScreenSize, verticalScreenSize), 0), Quaternion.identity);
    }

    void CreateCoin()
    {
        Instantiate(coinPrefab, new Vector3(Random.Range(-horizontalScreenSize * 0.8f, horizontalScreenSize * 0.8f), Random.Range(-verticalScreenSize, verticalScreenSize), 0), Quaternion.identity);
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
    private void Update()
    {
        if(gameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        gameOver = true;
    }

    IEnumerator SpawnPowerup()
    {
        float spawnTime = Random.Range(3, 5);
        yield return new WaitForSeconds(spawnTime);
        CreatePowerup();
        StartCoroutine(SpawnPowerup());
    }
    IEnumerator SpawnCoin()
    {
        float spawnTime = Random.Range(3, 5);
        yield return new WaitForSeconds(spawnTime);
        CreateCoin();
        StartCoroutine(SpawnCoin());
    }

    public void ManagePowerupText (int powerupType)
    {
        switch (powerupType)
        {
            case 1:
                powerUpText.text = "Speed!";
                break;
            case 2:
                powerUpText.text = "Double Weapon!";
                break;
            case 3:
                powerUpText.text = "Shield!";
                break;
            case 4:
                powerUpText.text = "Triple Weapon";
                break;
            default:
                powerUpText.text = "No power up";
                break;
        }
    }
    public void PlaySound(int whichSound)
    {
        switch (whichSound)
        {
            case 1:
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(powerupSound);
                break;
            case 2:
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(powerDownSound);
                break;
            case 3:
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(coinSound);
                break;
        }
    }
}