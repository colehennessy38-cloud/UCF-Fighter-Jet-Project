using UnityEngine;

public class ColeEnemy : MonoBehaviour
{
    public float speed;
    private GameManager gameManager;
    public GameObject explosionPrefab;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        if (transform.position.y >= gameManager.verticalScreenSize * 1.25f || transform.position.y <= -gameManager.verticalScreenSize * 1.25f)
        {
            Destroy(this.gameObject);
        }

    }
    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if (whatDidIHit.tag == "Weapons")
        {
            Destroy(whatDidIHit.gameObject);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            gameManager.RemoveScore(5);
            Destroy(this.gameObject);
        }

        if (whatDidIHit.tag == "Player")
        {
            whatDidIHit.GetComponent<PlayerController>().LoseALife();
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

    }
}
