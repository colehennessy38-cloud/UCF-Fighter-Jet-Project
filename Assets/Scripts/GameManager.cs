using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject enemyOnePrefab;
    public GameObject enemyTwoPrefab;
    public GameObject coleEnemyPrefab;
    public float horizontalScreenSize;
    public float verticalScreenSize;
    public GameObject cloudPrefab;

    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;

    public int score;

    void Start()
    {
        horizontalScreenSize = 10f;
        verticalScreenSize = 6.5f;

        InvokeRepeating("CreateEnemyOne", 1, 2);
        InvokeRepeating("CreateEnemyTwo", 5f, 3.5f);
        InvokeRepeating("CreateColeEnemy", 5, 3);

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
}