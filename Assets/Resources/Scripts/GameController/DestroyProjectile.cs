using UnityEngine;
using System.Collections;

public class DestroyProjectile : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Projectile" || other.gameObject.tag == "EnemyProjectile"){
			Destroy (other.gameObject);
		}
	}
}
