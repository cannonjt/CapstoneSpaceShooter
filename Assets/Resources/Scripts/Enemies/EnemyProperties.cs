using UnityEngine;
using System.Collections;
[System.Serializable]
public class EnemyProperties : MonoBehaviour {
	public float health;
	public float maxHealth;
	public GameObject explosion;
	public Color[] colors;

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
			if (!theThing.getDoH ())
			{
				//check if we're inv
				if (invToPirece)
				{
					//don't do anything
				}
				else
				{
					//we got hit, take damage and become immune
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
	void takeDamage(float damage)
	{
		health -= damage;
		if (health <= 0) {
				Destroy (gameObject);
				Instantiate (explosion, transform.position, transform.rotation);
		} else {
			StartCoroutine(flashRed());
		}
	}

	IEnumerator flashRed()
	{
		renderer.material.color = colors[0];
		yield return new WaitForSeconds(0.05f);
		renderer.material.color = colors[1];
	}

	void Update()
	{
		if (Time.time > invLength) 
		{
			invToPirece = false;
		}
	}
}
