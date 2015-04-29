using UnityEngine;
using System.Collections;

public class BossBehavior : MonoBehaviour {

	public Weapon currentWep;
	public float shootDist;

	private Transform player;

	// Use this for initialization
	void Start () {
		currentWep = (Weapon)Instantiate (currentWep);
		currentWep.GetComponent<Weapon> ().setUp (gameObject);
		currentWep.transform.parent = transform;

		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (getDistanceToPlayer () < shootDist) {
				currentWep.shoot ();
		}
	}

	//Calculates the distance between the target and its self
	float getDistanceToPlayer(){
		float distance;
		distance = Vector3.Distance (player.position, transform.position);
		return distance;
	}

	void OnDestroy(){
		GameObject.Find ("GameController").GetComponent<Victory>().victory ();
	}


}
