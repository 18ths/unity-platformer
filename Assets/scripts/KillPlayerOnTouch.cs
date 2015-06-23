using UnityEngine;
using System.Collections;

public class KillPlayerOnTouch : MonoBehaviour {

	public float levelLenght;
	public float killThreshold;

	void Start()
	{
		GetComponent<EdgeCollider2D>().points = 
			new Vector2[] {
				new Vector2(-levelLenght, killThreshold),
				new Vector2(levelLenght, killThreshold)
			};
	}

	void OnTriggerExit2D(Collider2D other)
	{
		Debug.Log("game over");
		Destroy(other.gameObject);
	}

}
