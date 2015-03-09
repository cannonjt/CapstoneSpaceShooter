using UnityEngine;
using System.Collections;
[System.Serializable]
public class EnemyHealth : MonoBehaviour {
	public float health;
	public float maxHealth;
	[HideInInspector]
	public bool invToPirece;
	[HideInInspector]
	public float invLength;
	void Start()
	{
		health = maxHealth;
	}
	void OnTriggerEnter(Collider other) 
	{
		if (other.tag == "Projectile") 
		{
			//Projectiles have damagers. I know, workaroundy, but best I could do
			GameObject bullet = other.gameObject;
			Damager theThing = bullet.GetComponent<Damager>();
			if (!theThing.destroyOnHit)
			{
				//check if we're inv
				if (invToPirece)
				{
					//don't do anything
				}
				else
				{
					//we got hit, take damage and become immune
					takeDamage(theThing.damage);
					invToPirece = true;
					invLength = Time.time + 1;
				}
			}
			else
			{
				takeDamage (theThing.damage);
				Destroy (other.gameObject);
			}
		}
	}
	void takeDamage(float damage)
	{
		health -= damage;
		if (health <= 0) 
		{
			Destroy (gameObject);
		}
	}

	void Update()
	{
		if (Time.time > invLength) 
		{
			invToPirece = false;
		}
	}
}
