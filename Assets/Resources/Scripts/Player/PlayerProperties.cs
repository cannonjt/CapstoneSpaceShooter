using UnityEngine;
using System.Collections;

public class PlayerProperties : MonoBehaviour {

	public int health;
	public int maxHealth;
	public GameObject weapon;
	public int shield;

	// Use this for initialization
	void Start () {
		health = maxHealth;
		shield = 50;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
