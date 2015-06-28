using UnityEngine;
using System.Collections;

public class CashGrab : MonoBehaviour {

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            gameManager.AddScore(1000);
            Destroy(gameObject);
        }
    }
}
