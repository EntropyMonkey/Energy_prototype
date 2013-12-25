using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour
{
	public enum Type { Earth };

	public float Pollution { get; set; } // between 0 and 1, 0 - not polluted

	[SerializeField]
	private Type type = Type.Earth;

	// in percent per second
	[SerializeField]
	private float pollutionGrowthRate = 1.0f / 60;

	[SerializeField]
	private float gainPollutionFromNeighborsPercent = 0.1f;

	// the current pollution change rate
	private float pollutionChangeRate = 0;

	// the current buffer for pollution. if above 1, starts to self-pollute
	private float pollutionBuffer = 0;

	// all neighbors
	private List<Tile> neighbors = new List<Tile>();

	void Awake()
	{
	}

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		// hasn't triggered self-pollution yet
		if (pollutionBuffer < 1)
		{
			renderer.material.color = Color.red * pollutionBuffer;
		}
		// has triggered self-pollution
		else
		{
			if (pollutionChangeRate == 0)
			{
				pollutionChangeRate = pollutionGrowthRate;
			}

			Pollution = Pollution + pollutionChangeRate * Time.deltaTime;

			renderer.material.color = new Color(Pollution, Pollution, Pollution);

			PolluteNeighbors();
		}
	}

	void PolluteNeighbors()
	{
		if (Pollution > 0.8f)
		{
			for (int i = 0; i < neighbors.Count; i++)
			{
				neighbors[i].pollutionBuffer += Pollution * neighbors[i].gainPollutionFromNeighborsPercent * Time.deltaTime;


			}
		}
	}

	//void CheckPollutedNeighbors()
	//{
	//	int pollutedNeighbors = 0;
	//	for (int i = 0; i < neighbors.Count; i++)
	//	{
	//		if (neighbors[i].Pollution >= 0.9f)
	//		{
	//			pollutedNeighbors++;
	//		}
	//	}

	//	if (pollutedNeighbors > 1)
	//	{
	//		Pollute();
	//	}
	//}

	public void AddNeighbor(Tile tile)
	{
		if (tile == null) return;

		if (!tile.neighbors.Contains(this))
			tile.neighbors.Add(this);

		if (!neighbors.Contains(tile))
			neighbors.Add(tile);
	}

	public void RemoveNeighbor(Tile tile)
	{
		tile.neighbors.Remove(this);
		neighbors.Remove(tile);
	}

	public void Pollute()
	{
		pollutionBuffer = 1.1f;
		pollutionChangeRate = pollutionGrowthRate;
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;

		foreach(Tile t in neighbors)
		{
			Gizmos.DrawLine(transform.position, t.transform.position);
		}
	}

	void OnMouseDown()
	{
		Pollute();
	}
}
