using UnityEngine;
using System.Collections;

public class Minigun : Weapon {
	
	public override void spawnBullet()
	{
		float i = shotProperties.firingAngle;
		float deviation = Random.Range (0.0f, i) - (i/2);
		//print (deviation);
		GameObject theBullet = (GameObject)Instantiate (shotProperties.shot, shotProperties.shotSpawn.position,
			                                                (shotProperties.shotSpawn.rotation 
			 * Quaternion.Euler(0f, deviation, 0f)));
			theBullet.GetComponent<Damager> ().setDamage(shotProperties.damage);
			theBullet.GetComponent<Damager> ().setDoH(shotProperties.destroyOnHit);
			
			organizeCategory(theBullet);
		
	}
}