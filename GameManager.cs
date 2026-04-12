using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject enemyOnePrefab;
    public GameObject coleEnemyPrefab;
    public float horizontalScreenSize;
    public float verticalScreenSize;
    public GameObject cloudPrefab;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;
    private int score = 0;
    void Start()
    {
        horizontalScreenSize = 10f;
        verticalScreenSize = 5f;
        InvokeRepeating("CreateEnemyOne", 1, 2);
        InvokeRepeating("CreateColeEnemy", 5, 3);
        CreateSky();
    }
    void CreateSky()
    {
        for (int i = 0; i < 30; i++)
        {
            Instantiate(cloudPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize), Random.Range(-verticalScreenSize, verticalScreenSize), 0), Quaternion.identity);
        }



    }
    void CreateEnemyOne()
    {
        Instantiate(enemyOnePrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) * .9f, verticalScreenSize, 0), Quaternion.identity);
    }

    void CreateColeEnemy()
    {
        Instantiate(coleEnemyPrefab, new Vector3(-horizontalScreenSize, Random.Range(0, verticalScreenSize), 0), Quaternion.identity);
    }

    void Update()
    {

    }
    public void ChangeLivesText(int currentLives)
    {
        livesText.text = "lives " + currentLives;
    }
    public void ChangeScoreText(int score)
    {
        scoreText.text = "score " + score;
    }

    public void AddScore(int earnedScore)
    {
        score = score + earnedScore;
    }
    public void RemoveScore(int earnedScore)
    {
        score = score - earnedScore;
    }

}
