using UnityEngine;
using System.Collections;

public class EnemyShootBehaviour : MonoBehaviour {

	public Weapon currentWep;
	public float shootDist;

	// Use this for initialization
	void Start () {
		currentWep = (Weapon)Instantiate (currentWep);
		currentWep.GetComponent<Weapon> ().setUp (gameObject);
		currentWep.transform.parent = transform;
	}
	
	// Update is called once per frame
	void Update () {

	}
}
