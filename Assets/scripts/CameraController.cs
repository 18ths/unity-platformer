using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject levelManager;

    private GameObject lookedAt;

    private Rect bounds;
    private float camSize;
    private float playerOffset;

    void Start()
    {
        lookedAt = levelManager.GetComponentInChildren<SpawnPlayer>().Spawn();

        bounds = levelManager.GetComponent<LevelManager>().levelBounds;
        camSize = GetComponent<Camera>().orthographicSize;
        playerOffset = lookedAt.GetComponent<BoxCollider2D>().size.x/2;
    }

	// Update is called once per frame
	void Update ()
	{
        if (lookedAt != null)
		{
            float posX = Mathf.Clamp(lookedAt.transform.position.x, bounds.xMin + (camSize - playerOffset), bounds.xMax - (camSize - playerOffset));

            //if player is not dead (its gameobject destroyed)
            transform.position = new Vector3 (posX, transform.position.y, transform.position.z);
		}
	}
}
