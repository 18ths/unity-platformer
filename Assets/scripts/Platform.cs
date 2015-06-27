using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

	public int platformLength;
	public GameObject tilePrefab;
	public GameObject firstTilePrefab;
	public GameObject lastTilePrefab;
    public bool isSolid;

	private float tileSizeX = 0;
	private float platformLength_px = 0;
	private ArrayList tiles = new ArrayList();

    [ShowOnly] public float tileLeftXPos;
    [ShowOnly] public float tileRightXPos;

    public void ClearPlatform()
	{
		foreach (GameObject tile in tiles)
		{
			DestroyImmediate(tile);
		}

		tiles.Clear();
	}

	public void CreatePlatform()
	{
		int iX = 0;
		int i = 0;
		
		tileSizeX = tilePrefab.GetComponent<SpriteRenderer> ().sprite.rect.width / 100;
		platformLength_px = tileSizeX * platformLength;
		
		while (iX < platformLength)
		{
			GameObject tile = GetTile(iX);
			
			tiles.Add(tile);
			tile.transform.parent = transform;
			tile.transform.name = "tile_"+i;
			i++;
			iX++;
		}
		
		float edgePoint = platformLength_px / 2;

        var colliders = GetComponents<EdgeCollider2D>();

        Debug.Assert(colliders.Length == 2, "There should be exactly 2 colliders!");

        colliders[0].points = new Vector2[] { new Vector2(-edgePoint,0), new Vector2(edgePoint, 0) };
        colliders[0].isTrigger = false;

        colliders[1].points = new Vector2[] { new Vector2(-edgePoint, -0.25f), new Vector2(edgePoint, -0.25f) };
        colliders[1].isTrigger = true;

        tileLeftXPos = transform.TransformPoint(colliders[0].points[0]).x;
        tileRightXPos = transform.TransformPoint(colliders[0].points[1]).x;
    }

	void Start() {
		ClearPlatform();
		CreatePlatform();	
	}

	GameObject GetTile(int index)
	{
		Object tile = tilePrefab;
		float positionX = 0.0f;

		if (index == 0) {
			tile = firstTilePrefab;
			positionX = (-platformLength_px + tileSizeX)/ 2 + transform.position.x;
		} else
			positionX = (tiles[index-1] as GameObject).transform.position.x + tileSizeX;

		if (index == platformLength - 1)
			tile = lastTilePrefab;

		return Instantiate(tile, new Vector3(positionX, transform.position.y, 0), Quaternion.Euler(0,0,0)) as GameObject;
	}
}
