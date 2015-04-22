using UnityEngine;
using System.Collections;


//class that will spawn floating asteroids
public class SpawnAsteroids : MonoBehaviour {

	public float spawnRate;
	public int numberSpawned;
	public Boundary spawnBound;
	public GameObject destructableAsteroid;
	public GameObject indestructableAsteroid;


	private Transform absBoundary;
	private float spawnTimer;


	// Use this for initialization
	void Start () {
		absBoundary = transform.GetChild (1);

		float xBoundSize = absBoundary.localScale.x;
		float zBoundSize = absBoundary.localScale.z;

		spawnBound.xMin = (-1f * xBoundSize / 2f) + 2f;
		spawnBound.xMax = (1f * xBoundSize / 2f) - 2f;
		spawnBound.zMin = (-1f * zBoundSize / 2f) + 2f;
		spawnBound.zMax = (1f * zBoundSize / 2f) - 2f;


		spawnTimer = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

		if (spawnTimer  <= Time.time ) {
			Vector3 spawnPos = getGoodSpawn();
			GameObject newAsteroid = (GameObject)Instantiate (destructableAsteroid, spawnPos, Quaternion.identity);

			spawnTimer = Time.time + spawnRate;
		}

	}

	/*
	Vector3 getRandomDirection(){

	}
	*/

	//returns a spawn position that is in the spawnBound
	//and not in the player's view
	Vector3 getGoodSpawn(){
		Vector3 spawnPos;

		while(true){


			//pick a random location to spawn
			spawnPos = new Vector3 (
				Random.Range (spawnBound.xMin, spawnBound.xMax), 
				0.0f,
				Random.Range (spawnBound.zMin, spawnBound.zMax)
				);
			//ensure spawn location is not in player's view
			if(!transform.GetChild(0).collider.bounds.Contains (spawnPos))
			{
				return spawnPos;
			}

		}
	}
}
