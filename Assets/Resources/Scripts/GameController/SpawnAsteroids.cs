using UnityEngine;
using System.Collections;


//class that will spawn floating asteroids
public class SpawnAsteroids : MonoBehaviour {

	public float spawnRate;
	public int maxAsteroids;
	public Boundary spawnBound;
	public float asteroidSpeed;
	public GameObject destructableAsteroid;
	public GameObject indestructableAsteroid;

	private GameObject parentAsteroid;


	private Transform absBoundary;
	private float spawnTimer;
	private int indestructableCounter;


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

		parentAsteroid = GameObject.Find("Asteroids");

		if (parentAsteroid == null) {
			parentAsteroid = new GameObject ("Asteroids");
		}

		indestructableCounter = 0;
	}

	
	// Update is called once per frame
	void Update () {

		if (spawnTimer  <= Time.time  && parentAsteroid.transform.childCount < maxAsteroids) {
			Vector3 spawnPos = getGoodSpawn();
			GameObject newAsteroid;

			if(indestructableCounter >= 4)//every fourth asteroid is indestructable
			{
				newAsteroid = (GameObject)Instantiate (indestructableAsteroid, spawnPos, Quaternion.identity);
				indestructableCounter = 0;
			}
			else
			{
				newAsteroid = (GameObject)Instantiate (destructableAsteroid, spawnPos, Quaternion.identity);
				indestructableCounter++;
			}
			Vector3 randomDirection = new Vector3(Random.Range(-75f, 75f), 0f, Random.Range(-75f, 75f));
			newAsteroid.rigidbody.AddForce(randomDirection * asteroidSpeed);
			spawnTimer = Time.time + spawnRate;

			newAsteroid.transform.parent = parentAsteroid.transform;
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
