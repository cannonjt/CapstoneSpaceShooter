using UnityEngine;
using System.Collections;

public class AsteroidHealth : MonoBehaviour {

	public float health;
	public float maxHealth;
	public GameObject explosion;
	
	[HideInInspector]
	public bool invToPirece;
	[HideInInspector]
	public float invLength;
	
	// Use this for initialization
	void Start () {
		health = maxHealth;
	}
	
	// Update is called once per frame
	void OnTriggerEnter(Collider other) 
	{
		if (other.tag == "Projectile") 
		{
			//Projectiles have damagers. I know, workaroundy, but best I could do
			GameObject bullet = other.gameObject;
			Damager theThing = bullet.GetComponent<Damager>();
			if (!theThing.getDoH ())
			{
				//check if we're inv
				if (!invToPirece)
				{
					takeDamage(theThing.getDamage());
					invToPirece = true;
					invLength = Time.time + 1;
				}
			}
			else
			{
				takeDamage (theThing.getDamage());
				Destroy (other.gameObject);
			}
		}
	}
	
	public void takeDamage(float damage)
	{
		health -= damage;
		if (health <= 0) {
			Destroy (gameObject);
			if(explosion != null)
				Instantiate (explosion, transform.position, transform.rotation);
		} 
	}

}
