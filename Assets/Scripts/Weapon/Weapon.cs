using UnityEngine;
using System.Collections;
[System.Serializable]
public class ShotProperties
{
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	
	//[HideInInspector]
	public float nextFire;
	void Awake()
	{
		shotSpawn = GameObject.Find("Player Shot Spawn").transform;
	}
}

public class Weapon : MonoBehaviour {
	
	public ShotProperties shotProperties;



	public void shoot()
	{
		if (Input.GetButton ("Fire1") && Time.time > shotProperties.nextFire) {
			shotProperties.nextFire = Time.time + shotProperties.fireRate;
			Instantiate (shotProperties.shot, shotProperties.shotSpawn.position,
			             shotProperties.shotSpawn.rotation);
			//audio.Play ();
		}
	}
	
}
