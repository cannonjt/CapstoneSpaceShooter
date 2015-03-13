using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public ShotProperties shotProperties;
	private GameObject user;
	
	void Awake()
	{

	}
	public virtual void shoot()
	{
		//print (shotProperties.shotSpawn);
		if (user.tag == "Player") {

			if ((Input.GetButton ("Fire1") || Input.GetAxisRaw ("FireHorizontal") != 0 
			     || Input.GetAxisRaw("FireVertical") != 0) && Time.time > shotProperties.nextFire) {
					shotProperties.nextFire = Time.time + shotProperties.fireRate;
					if (shotProperties.ammo == -5)
					{
						//default weapon has -5 ammo, so it doesn't use ammo
						spawnBullet ();
						audio.Play ();
					}
					else
					{
						//we have a weapon that shoots ammo
						//if it has 0 ammo left, switch to normal weapon
						if (shotProperties.ammo <= 0)
						{
							GameObject player = GameObject.FindGameObjectWithTag ("Player");
							PlayerController playerController = (PlayerController)player.GetComponent (typeof(PlayerController));
							playerController.resetWeapon ();
						}
						//else subtract 1 bullet
						else
						{
							shotProperties.ammo--;
							spawnBullet ();
							audio.Play ();
						}
					}
				}
		} else
		ungatedShoot ();
	}

	public virtual void spawnBullet()
	{
		//generates a Bullet
		GameObject theBullet = (GameObject)Instantiate (shotProperties.shot, shotProperties.shotSpawn.position,
		             shotProperties.shotSpawn.rotation);
		theBullet.GetComponent<Damager> ().setDamage(shotProperties.damage);
		theBullet.GetComponent<Damager> ().setDoH(shotProperties.destroyOnHit);
		organizeCategory (theBullet);

	}
	
	//call setUp before trying to use weapon
	//shot spawn must be the first child
	//sets the default spawn location to be first child
	public void setUp(GameObject weaponUser){
		user = weaponUser;
		Transform userTransform = user.transform;
		if (userTransform.childCount > 0) {
			setSpawnLocation(userTransform.GetChild(0).transform);
		}
		else{
			setSpawnLocation(userTransform);
		}

		shotProperties.nextFire = 0f;	
	}

	//sets the location that shots will spawn from
	public void setSpawnLocation(Transform weaponSpawnLocation){
		shotProperties.shotSpawn = weaponSpawnLocation;
	}

	void ungatedShoot()
	{
		if (Time.time > shotProperties.nextFire) {
			shotProperties.nextFire = Time.time + shotProperties.fireRate;
			spawnBullet ();
			audio.Play ();
		}
	}

	public void organizeCategory(GameObject theBullet)
	{
		theBullet.transform.parent = GameObject.Find ("Projectiles").transform;
	}
	
}
