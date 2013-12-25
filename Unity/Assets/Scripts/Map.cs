using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour
{
	[SerializeField]
	private Tile tilePrefab;

	[SerializeField]
	private float tileSize = 1;

	[SerializeField]
	private int mapSize = 10;

	Tile[,] tiles;

	// Use this for initialization
	void Start()
	{
		Generate(mapSize);
	}

	// Update is called once per frame
	void Update()
	{

	}

	void Generate(int size)
	{
		tiles = new Tile[size, size];

		Vector3 tPos = Vector3.zero;
		tPos.x = transform.position.x - (size - 1) * 0.5f * tileSize;
		tPos.z = transform.position.z - (size - 1) * 0.5f * tileSize;

		for (int x = 0; x < size; x++)
		{
			for (int y = 0; y < size; y++)
			{
				tiles[x, y] = Instantiate(tilePrefab, tPos, Quaternion.Euler(Vector3.right * 90)) as Tile;
				tiles[x, y].transform.parent = transform;
				tiles[x, y].name += " " + x + " " + y;

				if (x > 0)
				{
					if (y > 0)			tiles[x, y].AddNeighbor(tiles[x - 1, y - 1]);
										tiles[x, y].AddNeighbor(tiles[x - 1, y]);
					if (y + 1 < size)	tiles[x, y].AddNeighbor(tiles[x - 1, y + 1]);
				}

				if (y > 0)				tiles[x, y].AddNeighbor(tiles[x, y - 1]);
										tiles[x, y].AddNeighbor(tiles[x, y]);
				if (y + 1 < size)		tiles[x, y].AddNeighbor(tiles[x, y + 1]);

				if (x + 1 < size)
				{
					if (y > 0)			tiles[x, y].AddNeighbor(tiles[x + 1, y - 1]);
										tiles[x, y].AddNeighbor(tiles[x + 1, y]);
					if (y + 1 < size)	tiles[x, y].AddNeighbor(tiles[x + 1, y + 1]);
				}

				tPos.z += tileSize;
			}

			tPos.z = transform.position.z - (size - 1) * 0.5f * tileSize;
			tPos.x += tileSize;
		}
	}

	void Pollute(int x, int y)
	{
		tiles[x, y].Pollute();
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, Vector3.right * mapSize * tileSize + Vector3.forward * mapSize * tileSize);
	}
}
