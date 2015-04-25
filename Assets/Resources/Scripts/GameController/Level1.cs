using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Level1 : MonoBehaviour {
	public float initialCooldown;
	public float waveCooldown;
	public Boundary spawnBound;
	public int wave;
	public int totalWaves;

	private float cooldown;
	private bool check;
	private float timer;

	void Start () {
		wave = 0;
		check = false;
		initialCooldown = Time.time + 5;
	}

	void Update () {
		timer = Time.time;
		if (wave == totalWaves) 
		{
			return;
		}
		int enemiesLeft = GameObject.FindGameObjectsWithTag ("Enemy").Length;
		if (wave == 0){
			if (timer >= initialCooldown) {
					beginWave1 ();
					check = true;
				}
		}
		else 
		{
			Text wDisp = GameObject.Find ("WaveNum").GetComponent<Text> ();
			if (enemiesLeft == 0) 
			{
				wDisp.text = "Wave: " + wave.ToString();
				if (check)
				{
					cooldown = Time.time + waveCooldown;
					check = false;
				}
				else
				{
					if (timer >= cooldown)
					{
						check = true;
						if ((wave + 1) < totalWaves)
						{
							beginNextWave();
						}
						else
						{
							beginFinalWave();
							wDisp.text = "Wave: " + totalWaves.ToString() + "   Enemy: Boss";
						}
					}
				}
			}
			else
			{
				wDisp.text = "Wave: " + wave.ToString() + "   Enemies Left: " + enemiesLeft.ToString();
			}
		}
	}

	private void beginWave1()
	{
		wave = 1;
		for (int i=0; i<3; i++) 
		{
			spawnEnemy ();
		}
	}

	private void beginNextWave()
	{
		wave++;
		float variance, variance2, variance3;
		variance = Random.Range (-1.0f, 3.0f);
		variance2 = Random.Range (-3.0f, 1.0f);
		variance3 = Random.Range (-2.0f, 1.0f);
		for (int i=0; i<wave+variance; i++) 
		{
			spawnEnemy ();
		}
		for (int i=0; i<wave-3+variance2; i++) 
		{
			spawnEnemy2();
		}
		for (int i=0; i<wave-2+variance3; i++) 
		{
			spawnPair();
		}
	}
	private void beginFinalWave()
	{
		wave = totalWaves;
		spawnBoss ();
	}
	private void spawnEnemy()
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		GameObject spawningShip = (GameObject)Resources.Load ("Prefabs/Enemies/PurpleEnemy");
		Vector3 spawnPos = Vector3.zero;
		if (player != null) {
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
		}
		GameObject newEnemy = (GameObject)Instantiate (spawningShip, spawnPos, Quaternion.identity);
		if(player != null)
			newEnemy.GetComponent<TrackTo> ().target = player.transform;
	}
	private void spawnEnemy2()
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		GameObject spawningShip = (GameObject)Resources.Load ("Prefabs/Enemies/GreenEnemy");
		Vector3 spawnPos = Vector3.zero;
		if (player != null) {
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
		}
		GameObject newEnemy = (GameObject)Instantiate (spawningShip, spawnPos, Quaternion.identity);
		if(player != null)
			newEnemy.GetComponent<TrackTo2nd> ().target = player.transform;
	}
	private void spawnPair()
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		GameObject spawningShip = (GameObject)Resources.Load ("Prefabs/Enemies/PurpleEnemy");
		Vector3 spawnPos = Vector3.zero;
		if (player != null) {
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
		}
		GameObject newEnemy = (GameObject)Instantiate (spawningShip, spawnPos, Quaternion.identity);
		if(player != null)
			newEnemy.GetComponent<TrackTo> ().target = player.transform;
		GameObject follower = (GameObject)Resources.Load ("Prefabs/Enemies/EnemyFollower");
		GameObject newF = (GameObject)Instantiate (follower, spawnPos, Quaternion.identity);
		FollowerBehavior fBeh = newF.GetComponent<FollowerBehavior> ();
		fBeh.leader = newEnemy.transform;
	}
	private void spawnBoss()
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		GameObject spawningShip = (GameObject)Resources.Load ("Prefabs/Enemies/Boss");
		if (player != null) 
		{
			Rigidbody r = player.GetComponent<Rigidbody> ();
			Transform t = spawningShip.transform;
			GameObject g = t.GetChild (1).gameObject;
			ShootPredictively s = g.GetComponent<ShootPredictively> ();
			s.target = r;
		}
		Vector3 spawnPos = Vector3.zero;
		if (player != null) {
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
		}
		GameObject newEnemy = (GameObject)Instantiate (spawningShip, spawnPos, Quaternion.identity);
		if(player != null)
			newEnemy.GetComponent<TrackTo> ().target = player.transform;

	}
}
