using UnityEngine;
using System.Collections;

public class ExplosionSpawner : MonoBehaviour {

	public Boundary spawnBound;
	public GameObject explosion1;
	public GameObject explosion2;

	// Update is called once per frame
	void Update () {
		spawnExplosion (explosion1);
		spawnExplosion (explosion2);
	}

	//returns a spawn position that is in the spawnBound
	//and not in the player's view
	void spawnExplosion(GameObject explosion){
		Vector3 spawnPos;
		//pick a random location to spawn
		spawnPos = new Vector3 (
			Random.Range (spawnBound.xMin, spawnBound.xMax), 
			8f,
			Random.Range (spawnBound.zMin, spawnBound.zMax)
			);

		Instantiate (explosion, spawnPos, transform.rotation);

	}
}
