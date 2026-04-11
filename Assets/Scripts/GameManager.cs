using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject enemyOnePrefab;
    public float horizontalScreenSize;
    public float verticalScreenSize;
    public GameObject cloudPrefab;
    public TextMeshProUGUI livesText;
    public int score;
    void Start()
    {
        horizontalScreenSize = 10f;
        verticalScreenSize = 6.5f;
        InvokeRepeating("CreateEnemyOne", 1, 2);
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
        Instantiate(enemyOnePrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) *.9f, verticalScreenSize, 0), Quaternion.identity);
    }
    
    void Update()
    {
        
    }
    public void ChangeLivesText (int currentLives)
    {
        livesText.text = "lives " + currentLives;
    }
    public void AddScore(int earnedScore)
    {
        score = score + earnedScore;
    }
}
