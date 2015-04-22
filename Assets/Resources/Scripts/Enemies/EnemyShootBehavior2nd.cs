using UnityEngine;
using System.Collections;

public class EnemyShootBehavior2nd : MonoBehaviour {
	
	public Weapon currentWep;
	public float shootDist;
	public float nextCheck;
	public float fireRate;
	
	// Use this for initialization
	void Start () {
		nextCheck = Time.time + Random.Range (0.0f,1.0f);
		currentWep = (Weapon)Instantiate (currentWep);
		currentWep.GetComponent<Weapon> ().setUp (gameObject);
		currentWep.transform.parent = transform;
		
		fireRate = currentWep.shotProperties.fireRate;
	}
	
	// Update is called once per frame
	void Update () {
		Transform player = getTarget ();
		if (player.gameObject.activeInHierarchy) {
			if (Time.time > nextCheck) {
				int shoot = Random.Range (0, 10);
				
				if (shoot > 4) {
					currentWep.GetComponent<Weapon> ().shoot ();

				}
				nextCheck += fireRate;
			}
		}
	}
	
	Transform getTarget(){
		return gameObject.GetComponent<TrackTo2nd>().target;
	}
}

