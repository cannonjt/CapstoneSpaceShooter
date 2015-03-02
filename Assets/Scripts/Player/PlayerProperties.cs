using UnityEngine;
using System.Collections;

public class PlayerProperties : MonoBehaviour {

	public int health;
	public GameObject weapon;
	public int shield;

	// Use this for initialization
	void Start () {
		health = 100;
		shield = 50;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
