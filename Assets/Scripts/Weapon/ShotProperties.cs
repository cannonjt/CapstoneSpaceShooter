using UnityEngine;
using System.Collections;

[System.Serializable]
public class ShotProperties{

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	public int numberOfShots; //for multishot weapons
	public float firingAngle; //the maximum angle shots will fire at
	
	[HideInInspector]
	public float nextFire;
}
