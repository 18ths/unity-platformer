using UnityEngine;
using System.Collections;

public class LookAtObject : MonoBehaviour {

	public GameObject lookedAt;

	// Update is called once per frame
	void Update ()
	{
		//that happens if player is dead
		if (lookedAt != null)
		{
			transform.position = new Vector3 (lookedAt.transform.position.x, 
		                                   transform.position.y, 
		                                   transform.position.z);
		}
	}
}
