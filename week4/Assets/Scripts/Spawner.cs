using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	

	public float xSpawnPosMin; //left most spawn point
	public float xSpawnPosMax; //right most spawn point

	private float ySpawnPos; //height of spawn - either -0.6 or 6

	private float timeUntilSpawn;

	private float timeBetweenSpawns;

	public void Start()
	{
		//timeUntilSpawn = 1;
	}

	public void Update()
	{
		timeBetweenSpawns = Random.Range (0.3f, 0.7f);
		//Time.delaTime is how much time has occured since the last update. 
		//We subtract it from time until spawn every frame
		timeUntilSpawn -= Time.deltaTime;
		//Once timeUntilSpawn is less than 0, we spawn a new hat
		if (timeUntilSpawn <= 0)
		{
			SpawnThings();
			//then we reset timeUntilSpawn to the timeBetweenSpawns & start all over again
			timeUntilSpawn = timeBetweenSpawns;
		}
	}

	private void SpawnThings()
	{

		int r = Random.Range (0, 2);
		if (r == 0) {
			ySpawnPos= 6.0f;
		} else {
			ySpawnPos = -6f;
		}

		Vector3 newPos = new Vector3(Random.Range(xSpawnPosMin, xSpawnPosMax), ySpawnPos, 0);


		float possibility = Random.Range (0f, 10f);
		if (possibility < 6.5f) {
            Instantiate (Services.Prefabs.Enemy, newPos, Quaternion.identity);
		} else {
            Instantiate (Services.Prefabs.Ally, newPos, Quaternion.identity);
		}

	}
}
