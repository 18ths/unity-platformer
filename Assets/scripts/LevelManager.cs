using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public Rect levelBounds;

    private GameManager gameManager;
    private int rightSideColliderIndex;

	void Start()
	{
        var colliders = GetComponents<EdgeCollider2D>();

        Debug.Assert(colliders.Length == 3, "There should be exactly 2 colliders, trigger and nontrigger!");

        //down collider, should kill player on touch
        colliders[0].points = 
			new Vector2[] {
				new Vector2(levelBounds.xMin, levelBounds.yMin),
				new Vector2(levelBounds.xMax, levelBounds.yMin)
			};
        colliders[0].isTrigger = true;

        //left side collider, should stop player from moving
        colliders[1].points =
            new Vector2[] {
                        new Vector2(levelBounds.xMin, levelBounds.yMax),
                        new Vector2(levelBounds.xMin, levelBounds.yMin)
            };
        colliders[1].isTrigger = false;

        //right side collider, should stop player from moving
        colliders[2].points =
            new Vector2[] {
                        new Vector2(levelBounds.xMax, levelBounds.yMax),
                        new Vector2(levelBounds.xMax, levelBounds.yMin)
            };
        colliders[2].isTrigger = false;
        rightSideColliderIndex = 2;

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void KillPlayer(GameObject player, int currentLives)
    {
        Destroy(player);
        
        if (currentLives == 0)
            //TODO: get rid of getcomponentinchildren
            gameManager.GetComponentInChildren<GUIText>().text = "You died";
        else
        {
            gameManager.GetComponentInChildren<CameraController>().ResetTarget(gameObject.GetComponentInChildren<SpawnPlayer>().Spawn());
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.IsTouching(GetComponents<EdgeCollider2D>()[rightSideColliderIndex]))
        {
            //we are colliding with right side collider

            if (collision.collider.gameObject.tag == "Player")
                gameManager.Victory();
        }
    }

    void OnTriggerExit2D(Collider2D other)
	{
        if (other.gameObject.tag == "Player")
        {
            gameManager.KillPlayer(other.gameObject);
            return;
        }

        Destroy(other.gameObject);
    }
}
