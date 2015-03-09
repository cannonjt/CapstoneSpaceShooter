using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour {
	public float spawnCooldown;
	public Boundary spawnBound;
	public GameObject spawningShip;
	public int waveSize;
	
	private float spawnTimer;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		//spawn wave feature
		if (GameObject.FindGameObjectsWithTag ("Enemy").Length == 0) {

			//wait for spawn timer
			if(Time.time > spawnTimer)
			{
				spawn();
			}

		} else {
			spawnTimer = Time.time + spawnCooldown;
		}
	}

	//spawns a wave of enemies
	void spawn(){

		GameObject player = GameObject.FindGameObjectWithTag("Player");
		for (int i = 0; i < waveSize; i++) {
			GameObject newEnemy = (GameObject)Instantiate (spawningShip);
			Vector3 spawnPos = new Vector3 (
				Random.Range (spawnBound.xMin, spawnBound.xMax), 
				0.0f,
				Random.Range (spawnBound.zMin, spawnBound.zMax)
			);

			newEnemy.rigidbody.position = spawnPos;
			newEnemy.GetComponent<TrackTo> ().target = player.transform;
		}
	}
}
