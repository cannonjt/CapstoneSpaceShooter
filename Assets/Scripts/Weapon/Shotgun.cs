using UnityEngine;
using System.Collections;

public class Shotgun : Weapon {

	public override void spawnBullet()
	{
		float degreeSegment = shotProperties.firingAngle / (shotProperties.numberOfShots - 1);

		for(float i = 0f; i < shotProperties.numberOfShots; i++)
		{
			Instantiate (shotProperties.shot, shotProperties.shotSpawn.position,
	             (shotProperties.shotSpawn.rotation 
				 * Quaternion.Euler(0f, -(shotProperties.firingAngle / 2f) 
			                   + (i * degreeSegment), 0f)));
		}

	}
}
