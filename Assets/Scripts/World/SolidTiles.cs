using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SolidTiles : MonoBehaviour
{

	Location_Grid location_Grid;
	public int locX, locY;

	BoundsInt bounds = new BoundsInt(0, 0, 0, 32, 32, 1);
	Tilemap[] tilemaps;
	private bool[] mapArray;

	// Use this for initialization
	void Start()
	{
		tilemaps = new Tilemap[transform.childCount];
		mapArray = new bool[32 * 32];
		int iterator = 0;
		foreach (Transform child in transform)
		{
			tilemaps[iterator] = child.gameObject.GetComponent<Tilemap>();
			iterator++;
		}
		location_Grid = transform.parent.GetComponent<Location_Grid>();
		location_Grid.grid[locX, locY] = transform.gameObject.GetComponent<Grid>();
		foreach (Tilemap tilemap in tilemaps)
		{
			TileBase[] tiles = tilemap.GetTilesBlock(bounds);
			for (int i = 0; i < tiles.Length; i++)
			{
				if (tiles[i] != null)               
					mapArray[i] = true;
			}
		}
	}
    
    public bool CanMove(int x, int y)
	{   
		if (x < 0 || y < 0)
			return true;   
		return mapArray[x + y * 32];
	}
}