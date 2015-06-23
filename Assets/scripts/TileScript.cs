using UnityEngine;
using System.Collections;

public class TileScript : MonoBehaviour {

	public int platformLength;
	public GameObject tilePrefab;
	public GameObject firstTilePrefab;
	public GameObject lastTilePrefab;

	private float tileSizeX = 0;
	private float platformLength_px = 0;
	private ArrayList tiles = new ArrayList();

	public void ClearPlatform()
	{
		foreach (GameObject tile in tiles)
		{
			DestroyImmediate(tile);
		}

		tiles.Clear();
	}

	public void DrawPlatform()
	{
		int iX = 0;
		int i = 0;
		
		tileSizeX = tilePrefab.GetComponent<SpriteRenderer> ().sprite.rect.width / 100;
		platformLength_px = tileSizeX * platformLength;
		
		while (iX < platformLength)
		{
			Debug.Log(iX);
			GameObject tile = GetTile(iX);
			
			tiles.Add(tile);
			tile.transform.parent = transform;
			tile.transform.name = "tile_"+i;
			i++;
			iX++;
		}
		
		float edgePoint = (platformLength_px - tileSizeX) / 2;
		
		GetComponent<EdgeCollider2D>().points = new Vector2[]{new Vector2(-edgePoint,0), new Vector2(edgePoint, 0)};

	}

	void Start() {
		DrawPlatform();	
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
