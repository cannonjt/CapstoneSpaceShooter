using UnityEngine;
using System.Collections;

public class DestroyProjectile : MonoBehaviour {

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Projectile" || other.gameObject.tag == "EnemyProjectile"){
			Destroy (other.gameObject);
		}
	}
}
