using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Level1 : MonoBehaviour {
	//Time from when scene is loaded to first wave spawning in
	public float initialCooldown;
	//Cooldown time between waves
	public float waveCooldown;
	//Enemies can only spawn within this boundary
	public Boundary spawnBound;
	//Current wave number. Not sure why this is public tbh
	public int wave;
	//Total number of waves, final wave guaranteed to be a boss
	public int totalWaves;

	//Counter variable during cooldown
	private float cooldown;
	//Bool variable to check if we can spawn next wave
	private bool check;
	//Keeps track of the time
	private float timer;

	//GameObject variable to interact with the radar
	private GameObject radar;

	//Keeps track of enemy number to assign unique ID
	private int currentEnemyID;

	void Start () {
		//Initialize some necessary variables
		wave = 0;
		check = false;
		initialCooldown = Time.time + initialCooldown;
		radar = GameObject.Find ("Radar");
		currentEnemyID = 0;
	}

	void Update () {
		//Update timer
		timer = Time.time;
		//If player is dead, don't want to update
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if (player == null) {return;}
		//Are we in boss wave?
		if (wave == totalWaves) 
		{
			return;
		}
		//Count # of enemies left. Will be important in determining if we can proceed to next wave
		int enemiesLeft = GameObject.FindGameObjectsWithTag ("Enemy").Length;
		if (wave == 0){
			//Wave 1. After cooldown, begin it
			if (timer >= initialCooldown) {
					beginWave1 ();
					check = true;
				}
		}
		else 
		{
			//We're not in pre-game. First, update GUI to say wave # and enemies left, if any are left
			Text wDisp = GameObject.Find ("WaveNum").GetComponent<Text> ();
			if (enemiesLeft == 0) 
			{
				//All enemies are dead, so we're in cooldown. 
				//First time we enter cooldown, make sure to set our cooldown timer
				wDisp.text = "Wave: " + wave.ToString();
				if (check)
				{
					cooldown = Time.time + waveCooldown;
					check = false;
				}
				else
				{
					//Begin the next wave!
					if (timer >= cooldown)
					{
						check = true;
						if ((wave + 1) < totalWaves)
						{
							beginNextWave();
						}
						else
						{
							//This is the last wave, so it calls a different method
							beginFinalWave();
							wDisp.text = "Wave: " + totalWaves.ToString() + "   Enemy: Boss";
						}
					}
				}
			}
			else
			{
				//Enemies are still alive. Show how many in GUI.
				wDisp.text = "Wave: " + wave.ToString() + "   Enemies Left: " + enemiesLeft.ToString();
			}
		}
	}

	private void beginWave1()
	{
		//Spawn 3 basic enemies.
		wave = 1;
		for (int i=0; i<3; i++) 
		{
			spawnEnemy ();
		}
	}

	private void beginNextWave()
	{
		//A wave will contain a random amount of enemies.
		wave++;
		float variance, variance2, variance3;
		variance = Random.Range (-1.0f, 3.0f);
		variance2 = Random.Range (-3.0f, 1.0f);
		variance3 = Random.Range (-2.0f, 1.0f);
		//Basic enemy: We can spawn a lot of these.
		for (int i=0; i<wave+variance; i++) 
		{
			spawnEnemy ();
		}
		//The green enemy. Rather rare, but can potentially spawn ~wave 3
		for (int i=0; i<wave-3+variance2; i++) 
		{
			spawnEnemy2();
		}
		//A pair of a normal enemy and a follower.
		for (int i=0; i<wave-2+variance3; i++) 
		{
			spawnPair();
		}
	}

	//Private method to spawn the boss
	private void beginFinalWave()
	{
		//Spawn the boss!
		wave = totalWaves;
		spawnBoss ();
	}

	//Method to spawn a basic enemy.
	private void spawnEnemy()
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if (player == null) {return;}
		GameObject spawningShip = (GameObject)Resources.Load ("Prefabs/Enemies/PurpleEnemy");
		Vector3 spawnPos = Vector3.zero;
		//If there's a player, we need to make sure the enemies won't spawn in the player's fov.
			float xVal = Random.Range (spawnBound.xMin, spawnBound.xMax);
			while (player.transform.position.x - xVal < 5.5 && player.transform.position.x - xVal > -5.5) {
				//get a new xval
				xVal = Random.Range (spawnBound.xMin, spawnBound.xMax);
			}
			float zVal = Random.Range (spawnBound.zMin, spawnBound.zMax);
			while (player.transform.position.z - zVal < 5.5 && player.transform.position.z - zVal > -5.5) {
				//get a new zval
				zVal = Random.Range (spawnBound.zMin, spawnBound.zMax);
			}
			spawnPos = new Vector3 (
				xVal, 
				0.0f,
			zVal);
		GameObject newEnemy = (GameObject)Instantiate (spawningShip, spawnPos, Quaternion.identity);
		//Make sure they track the player!
		newEnemy.GetComponent<TrackTo> ().target = player.transform;

		//Adds the new enemy to the radar track Hashtable
		addEnemyToRadar (newEnemy);
	}
	private void spawnEnemy2()
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if (player == null) {return;}
		GameObject spawningShip = (GameObject)Resources.Load ("Prefabs/Enemies/GreenEnemy");
		Vector3 spawnPos = Vector3.zero;
		//If there's a player, we need to make sure the enemies won't spawn in the player's fov.
			float xVal = Random.Range (spawnBound.xMin, spawnBound.xMax);
			while (player.transform.position.x - xVal < 5.5 && player.transform.position.x - xVal > -5.5) {
				//get a new xval
				xVal = Random.Range (spawnBound.xMin, spawnBound.xMax);
			}
			float zVal = Random.Range (spawnBound.zMin, spawnBound.zMax);
			while (player.transform.position.z - zVal < 5.5 && player.transform.position.z - zVal > -5.5) {
				//get a new zval
				zVal = Random.Range (spawnBound.zMin, spawnBound.zMax);
			}
			spawnPos = new Vector3 (
				xVal, 
				0.0f,
				zVal);
		GameObject newEnemy = (GameObject)Instantiate (spawningShip, spawnPos, Quaternion.identity);
		//Make sure they track the player!
			newEnemy.GetComponent<TrackTo2nd> ().target = player.transform;

		//Adds the new enemy to the radar track Hashtable
		addEnemyToRadar (newEnemy);
	}

	private void spawnPair()
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if (player == null) {return;}
		GameObject spawningShip = (GameObject)Resources.Load ("Prefabs/Enemies/PurpleEnemy");
		Vector3 spawnPos = Vector3.zero;
		//If there's a player, we need to make sure the enemies won't spawn in the player's fov.
			float xVal = Random.Range (spawnBound.xMin, spawnBound.xMax);
			while (player.transform.position.x - xVal < 5.5 && player.transform.position.x - xVal > -5.5) {
				//get a new xval
				xVal = Random.Range (spawnBound.xMin, spawnBound.xMax);
			}
			float zVal = Random.Range (spawnBound.zMin, spawnBound.zMax);
			while (player.transform.position.z - zVal < 5.5 && player.transform.position.z - zVal > -5.5) {
				//get a new zval
				zVal = Random.Range (spawnBound.zMin, spawnBound.zMax);
			}
			spawnPos = new Vector3 (
				xVal, 
				0.0f,
				zVal);
		GameObject newEnemy = (GameObject)Instantiate (spawningShip, spawnPos, Quaternion.identity);
		//Make sure they track the player!
			newEnemy.GetComponent<TrackTo> ().target = player.transform;
		//The follower is a lot easier to instantiate, since it will spawn next to it's parent
		GameObject follower = (GameObject)Resources.Load ("Prefabs/Enemies/EnemyFollower");
		GameObject newF = (GameObject)Instantiate (follower, spawnPos, Quaternion.identity);
		FollowerBehavior fBeh = newF.GetComponent<FollowerBehavior> ();
		fBeh.leader = newEnemy.transform;

		//Adds the new enemy to the radar track Hashtable
		addEnemyToRadar (newEnemy);
	}

	private void spawnBoss()
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if (player == null) {return;}
		GameObject spawningShip = (GameObject)Resources.Load ("Prefabs/Enemies/Boss");
		//This portion is for the predictive cannon the boss has
			Rigidbody r = player.GetComponent<Rigidbody> ();
			Transform t = spawningShip.transform;
			GameObject g = t.GetChild (1).gameObject;
			ShootPredictively s = g.GetComponent<ShootPredictively> ();
			s.target = r;
		Vector3 spawnPos = Vector3.zero;
		//If there's a player, we need to make sure the enemies won't spawn in the player's fov.
			float xVal = Random.Range (spawnBound.xMin, spawnBound.xMax);
			while (player.transform.position.x - xVal < 5.5 && player.transform.position.x - xVal > -5.5) {
				//get a new xval
				xVal = Random.Range (spawnBound.xMin, spawnBound.xMax);
			}
			float zVal = Random.Range (spawnBound.zMin, spawnBound.zMax);
			while (player.transform.position.z - zVal < 5.5 && player.transform.position.z - zVal > -5.5) {
				//get a new zval
				zVal = Random.Range (spawnBound.zMin, spawnBound.zMax);
			}
			spawnPos = new Vector3 (
				xVal, 
				0.0f,
				zVal);
		GameObject newEnemy = (GameObject)Instantiate (spawningShip, spawnPos, Quaternion.identity);
		//Make sure they track the player!
		newEnemy.GetComponent<TrackTo> ().target = player.transform;

		//Adds the new enemy to the radar track Hashtable
		addEnemyToRadar (newEnemy);
	}

	private void addEnemyToRadar(GameObject currentEnemy){
		currentEnemy.GetComponent<EnemyProperties> ().setEnemyID (currentEnemyID);
		radar.GetComponent<Radar> ().addEnemy (currentEnemyID, currentEnemy);
		currentEnemyID++;
	}
}
