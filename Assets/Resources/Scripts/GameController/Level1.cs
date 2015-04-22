using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Level1 : MonoBehaviour {
	public float initialCooldown;
	public float waveCooldown;
	public Boundary spawnBound;
	public int wave;

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
						beginNextWave();
						check = true;
					}
				}
			}
			else
			{
				wDisp.text = "Wave: " + wave.ToString() + " Enemies Left: " + enemiesLeft.ToString();
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
		for (int i=0; i<wave+2; i++) 
		{
			spawnEnemy ();
		}
		for (int i=0; i<wave-1; i++) 
		{
			spawnPair();
		}
	}
	private void spawnEnemy()
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		GameObject spawningShip = (GameObject)Resources.Load ("Prefabs/Enemies/PurpleEnemy");
		Vector3 spawnPos = new Vector3 (
				Random.Range (spawnBound.xMin, spawnBound.xMax), 
				0.0f,
			Random.Range (spawnBound.zMin, spawnBound.zMax));
		GameObject newEnemy = (GameObject)Instantiate (spawningShip, spawnPos, Quaternion.identity);
		if(player != null)
			newEnemy.GetComponent<TrackTo> ().target = player.transform;
	}
	private void spawnPair()
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		GameObject spawningShip = (GameObject)Resources.Load ("Prefabs/Enemies/PurpleEnemy");
		Vector3 spawnPos = new Vector3 (
			Random.Range (spawnBound.xMin, spawnBound.xMax), 
			0.0f,
			Random.Range (spawnBound.zMin, spawnBound.zMax));
		GameObject newEnemy = (GameObject)Instantiate (spawningShip, spawnPos, Quaternion.identity);
		if(player != null)
			newEnemy.GetComponent<TrackTo> ().target = player.transform;
		GameObject follower = (GameObject)Resources.Load ("Prefabs/Enemies/EnemyFollower");
		GameObject newF = (GameObject)Instantiate (follower, spawnPos, Quaternion.identity);
		FollowerBehavior fBeh = newF.GetComponent<FollowerBehavior> ();
		fBeh.leader = newEnemy.transform;
	}
}
