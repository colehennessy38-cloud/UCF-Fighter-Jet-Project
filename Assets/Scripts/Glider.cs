using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glider : MonoBehaviour
{
    public bool goingUp;
    public float speed;
    private GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (goingUp)
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);

        }
        else if(goingUp == false)
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);

        }

        if (transform.position.y < -gameManager.verticalScreenSize *1.25|| transform.position.y > gameManager.verticalScreenSize * 1.25)
        {
            Destroy(this.gameObject);


        }

        /*transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * 3f);
        if(transform.position.y < -6.5f)
        {
            Destroy(this.gameObject);
        
        
        }
        */

    }
}
