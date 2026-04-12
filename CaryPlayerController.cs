using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //teleproting and movement get clumped together
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject bulletPrefab;
    private float playerSpeed;
    private float horizontalInput;
    private float verticalInput;
    public int lives;
    private int score = 0;
    public GameManager gameManager;
    public GameObject explosionPrefab;






    void Start()
    {
        playerSpeed = 6f;
        lives = 3;
        score = 0;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.ChangeLivesText(lives);
        gameManager.ChangeScoreText(score);
        

    }

    // Update is called once per frame
    void Update()
    {
        Shooting();
        Movement();
    }
    public void LoseALife()
    {
        lives--;
        gameManager.ChangeLivesText(lives);
        if(lives == 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
   public void GainScore()
    {
        score += 5;
        gameManager.AddScore(score);
    }
    public void LoseScore()
    {
        score -= 5;
        gameManager.RemoveScore(score);
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
            transform.position = new Vector3(transform.position.x * -1, transform.position.y,  0);
        }
        
        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, -0.1f, 0);
        }
        if (transform.position.y <= -verticalScreenSize)
        {
            transform.position = new Vector3(transform.position.x, -verticalScreenSize + 0.1f, 0);
        }
    }
    void Shooting()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {//1 what we are spawning, position where we spawn it, rotation we spawn it at
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }
    }
   
}
