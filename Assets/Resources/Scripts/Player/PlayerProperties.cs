﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerProperties : MonoBehaviour {

	public Slider hSlider;
	public Slider sSlider;
	public float health;
	public float maxHealth;
	public float shield;
	public float maxShield;
	public float shieldRechargeTime;
	public float shieldRechargeRate;
	public float invPeriod;//period where the player is invicible after taking damage
	public GameObject gameOverText; //text that will display when the player dies

	private float lastDamage;
	private GameObject explosion;
	private GameObject shieldObject;
	private bool atMax;


	// Use this for initialization
	void Start () {
		health = maxHealth;
		shield = maxShield;

		explosion = (GameObject)Resources.Load ("Prefabs/VFX/Explosions/explosion_player");

		shieldObject = transform.GetChild(2).gameObject;
		lastDamage = Time.time;

	}
	
	// Update is called once per frame
	void Update () {
		atMax = (health >= maxHealth);
		//if shield is not maxed and shield cooldown is up
		if(shield < maxShield && lastDamage <= (Time.time - shieldRechargeTime))
		{
			float newShield = shield + (shieldRechargeRate * Time.deltaTime);
			if(newShield > maxShield)shield = maxShield;
			else shield = newShield;

		}
		if (shield <= 0) {
			sSlider.value = 0;
		}
		else
		{
			sSlider.value = shield;
		}
		if (health <= 0) {
			hSlider.value = 0;
		}
		else
		{
			hSlider.value = health;
		}
	}

	void LateUpdate()
	{
		shieldObject.transform.rotation = Quaternion.identity;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "EnemyProjectile" || other.tag == "KillerProjectile") {
			//Projectiles have damagers. I know, workaroundy, but best I could do
			GameObject bullet = other.gameObject;
			Damager theThing = bullet.GetComponent<Damager>();

			takeDamage (theThing.getDamage());
			Destroy (other.gameObject);
		
		}

	}

	void OnCollisionEnter(Collision otherCollision){

		Collider other = otherCollision.collider;
		if(other.tag == "Asteroid" || other.tag == "Enemy"){
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
		if (Time.time >= lastDamage + invPeriod) {
			lastDamage = Time.time;

			//damage shield first, then health
			if (damage > shield) {
				health -= (damage - shield);
				shield = 0;
				//fixing a quirk with slider, since it won't call update
				sSlider.value = 0;
				StartCoroutine (flashRed ());
			} else {
				shield -= damage;
				StartCoroutine (flashShield ());
			}

			if (health <= 0) {
				hSlider.value = 0;
				gameOverText.SetActive (true);
				gameObject.SetActive (false);
				Instantiate (explosion, transform.position, transform.rotation);
			}
		}

	}

	public void healFor(float healHealth)
	{
		if ((health + healHealth) > maxHealth)
						health = maxHealth;
				else
						health += healHealth;
	}

	public bool getMax()
	{
		return atMax;
	}

	IEnumerator flashRed()
	{
		Image red = GameObject.Find("Redness").GetComponent<Image>();
		Color c = red.color;
		c.a = .5f;
		red.color = c;
		renderer.material.color = Color.red;
		yield return new WaitForSeconds(0.05f);
		renderer.material.color = Color.white;
		c.a = 0;
		red.color = c;
	}

	IEnumerator flashShield()
	{
		Transform shieldTrans = shieldObject.transform;
		shieldTrans.gameObject.SetActive (true);
		yield return new WaitForSeconds(0.05f);
		shieldTrans.gameObject.SetActive (false);
	}
	
}
