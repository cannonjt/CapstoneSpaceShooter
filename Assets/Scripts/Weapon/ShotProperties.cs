using UnityEngine;
using System.Collections;

[System.Serializable]
public class ShotProperties{

	public GameObject shot;
	public float fireRate;
	public int numberOfShots; //for multishot weapons
	public float firingAngle; //the maximum angle shots will fire at
	public float range;
	
	[HideInInspector]
	public float nextFire;
	[HideInInspector]
	public Transform shotSpawn;
}
