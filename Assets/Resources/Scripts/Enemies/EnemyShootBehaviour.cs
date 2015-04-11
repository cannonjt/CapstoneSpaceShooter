using UnityEngine;
using System.Collections;

public class EnemyShootBehaviour : MonoBehaviour {

	public Weapon currentWepLeft;
	public Weapon currentWepRight;
	public float shootDist;
	public float nextCheck;
	public float fireRate;

	// Use this for initialization
	void Start () {
		nextCheck = Time.time + Random.Range (0.0f,1.0f);
		currentWepLeft = (Weapon)Instantiate (currentWepLeft);
		currentWepLeft.GetComponent<Weapon> ().setUp (gameObject);
		currentWepLeft.transform.parent = transform;

		currentWepRight = (Weapon)Instantiate (currentWepRight);
		currentWepRight.GetComponent<Weapon> ().setUp (gameObject);
		Transform rightSpawn = transform.GetChild (1);
		currentWepRight.GetComponent<Weapon> ().setSpawnLocation (rightSpawn);
		currentWepRight.transform.parent = transform;

		fireRate = currentWepRight.shotProperties.fireRate;
	}
	
	// Update is called once per frame
	void Update () {
		Transform player = getTarget ();
		if (player.gameObject.activeInHierarchy) {
			if (Time.time > nextCheck) {
				int shoot = Random.Range (0, 10);

				if (shoot > 4) {
					currentWepLeft.GetComponent<Weapon> ().shoot ();
					currentWepRight.GetComponent<Weapon> ().shoot ();
				}
				nextCheck += fireRate;
			}
		}
	}

	Transform getTarget(){
			return gameObject.GetComponent<TrackTo>().target;
		}
}
