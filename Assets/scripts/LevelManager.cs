using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public Rect levelBounds;

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
    }

    public void KillPlayer(GameObject player)
    {
        GetComponentInChildren<GUIText>().text = "umarłeś, beka z typa xD";
        Destroy(player);
    }

	void OnTriggerExit2D(Collider2D other)
	{
        if (other.gameObject.tag == "Player")
        {
            KillPlayer(other.gameObject);
            //we will handle player gameobject destroy in killPlayer method
            return;
        }

        Destroy(other.gameObject);
    }
}
