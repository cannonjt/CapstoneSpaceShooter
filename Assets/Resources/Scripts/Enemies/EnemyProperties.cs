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

	private FollowerBehavior followerInfo;
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

	void OnCollisionEnter(Collision otherCollision){
		
		Collider other = otherCollision.collider;
		if(other.tag == "Asteroid" || other.tag == "Player"){
			//print ("ouch, IM HIT!!!!!" + other.tag);
			GameObject asteroid = other.gameObject;
			Damager asteroidDamager = asteroid.GetComponent<Damager>();
			
			takeDamage (asteroidDamager.getDamage());
			
			if(asteroidDamager.getDoH()){
				Destroy (other.gameObject);
				GameObject aExplosion =(GameObject)Resources.Load("Prefabs/VFX/Explosions/explosion_asteroid");
				Instantiate (aExplosion, other.transform.position, other.transform.rotation);
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
		} else {
			StartCoroutine(flashRed());
		}
	}

	IEnumerator flashRed()
	{
		if(colors.Length >= 2)
		{
			renderer.material.color = colors[0];
			yield return new WaitForSeconds(0.05f);
			renderer.material.color = colors[1];
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
