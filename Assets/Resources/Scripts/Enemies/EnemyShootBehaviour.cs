using UnityEngine;
using System.Collections;

public class EnemyShootBehaviour : MonoBehaviour {

	public Weapon currentWepLeft;
	public Weapon currentWepRight;
	public float shootDist;

	// Use this for initialization
	void Start () {
		currentWepLeft = (Weapon)Instantiate (currentWepLeft);
		currentWepLeft.GetComponent<Weapon> ().setUp (gameObject);
		currentWepLeft.transform.parent = transform;

		currentWepRight = (Weapon)Instantiate (currentWepRight);
		currentWepRight.GetComponent<Weapon> ().setUp (gameObject);
		Transform rightSpawn = transform.GetChild (1);
		currentWepRight.GetComponent<Weapon> ().setSpawnLocation (rightSpawn);
		currentWepRight.transform.parent = transform;
	}
	
	// Update is called once per frame
	void Update () {
		currentWepLeft.GetComponent<Weapon> ().shoot();
		currentWepRight.GetComponent<Weapon> ().shoot();
	}
}
