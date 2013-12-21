using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{
	public enum Type { Earth };

	public float Pollution { get; set; } // between 0 and 1, 0 - not polluted

	[SerializeField]
	private Type type = Type.Earth;

	// in percent per second
	[SerializeField]
	public float pollutionGrowthRate = 1.0f / 120;

	// the current pollution change rate
	private float pollutionChangeRate = 0;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		Pollution = Pollution + pollutionChangeRate * Time.deltaTime;
		
		renderer.material.color = new Color(1 - Pollution, 1 - Pollution, 1 - Pollution);
	}

	public void Pollute()
	{
		pollutionChangeRate = pollutionGrowthRate;
	}
}
