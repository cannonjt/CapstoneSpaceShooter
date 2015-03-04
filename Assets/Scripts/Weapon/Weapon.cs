using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	
	public ShotProperties shotProperties;


	void Awake()
	{
		shotProperties.shotSpawn = GameObject.Find("Player Shot Spawn").transform;
	}
	public virtual void shoot()
	{
		//print (shotProperties.shotSpawn);
		if (Input.GetButton ("Fire1") && Time.time > shotProperties.nextFire) {
			shotProperties.nextFire = Time.time + shotProperties.fireRate;
			spawnBullet();
			//audio.Play ();
		}
	}

	public virtual void spawnBullet()
	{
		//generates a Bullet
		Instantiate (shotProperties.shot, shotProperties.shotSpawn.position,
		             shotProperties.shotSpawn.rotation);
	}
	
}
