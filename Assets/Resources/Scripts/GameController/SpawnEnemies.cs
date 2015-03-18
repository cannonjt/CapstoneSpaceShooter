using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour {
	public float spawnCooldown;
	public Boundary spawnBound;
	public GameObject spawningShip;
	public GameObject spawningWeapon;
	public int waveSize;
	
	private float spawnTimer;
	private int wave;

	// Use this for initialization
	void Start () {
		wave = 0;
	}
	
	// Update is called once per frame
	void Update () {

		//spawn wave feature
		if (GameObject.FindGameObjectsWithTag ("Enemy").Length == 0) {

			//wait for spawn timer
			if(Time.time > spawnTimer)
			{
				spawn();
				wave++;
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
			if(player != null)
				newEnemy.GetComponent<TrackTo> ().target = player.transform;
		}

		if (wave % 2 == 0) {
			GameObject newWeapon = (GameObject)Instantiate (spawningWeapon);
				
			Vector3 spawnPos = new Vector3 (
				Random.Range (spawnBound.xMin, spawnBound.xMax), 
				0.0f,
				Random.Range (spawnBound.zMin, spawnBound.zMax)
				);
			newWeapon.rigidbody.position = spawnPos;
		}

	}
}
