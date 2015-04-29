using UnityEngine;
using System.Collections;

[System.Serializable]
public class ShotProperties{

	public GameObject shot;
	public float ammo;
	public float damage;
	public float fireRate;
	public int numberOfShots; //for multishot weapons
	public float firingAngle; //the maximum angle shots will fire at
	public int burst; //the number of shots in each burst (0 for no burst fire)
	public float burstWait; //the wait time in between each burst
	public float range;
	public bool destroyOnHit;
	public bool oneLoop;


	[HideInInspector]
	public float nextFire;
	[HideInInspector]
	public float nextBurst;
	[HideInInspector]
	public Transform shotSpawn;
}
