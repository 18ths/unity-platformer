using UnityEngine;
using System.Collections;

public class EnemyCollisionHandler : MonoBehaviour {

    private LevelManager levelManager;

    public float jumpKillFactor;

    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            Vector2 posDiff = collision.collider.gameObject.transform.position - transform.position;
            Vector2 direction = posDiff.normalized;

            // in normalized vector sum of its fabs(components) will be equal to 1
            // if both components are 0.5 we can assume the attack came from above in 45 degree cone
            if (direction.y > 0.5f) 
            {
                //death from above!
                Destroy(gameObject);
                collision.collider.gameObject.SendMessage("ForceJump", jumpKillFactor);
                return;
            }
            else
                levelManager.KillPlayer(collision.collider.gameObject);
        }
    }
}
