using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
	void OnTriggerEnter(Collider other) 
	{
		if (other.tag == "Projectile") {
						Destroy (gameObject);
			Destroy (other.gameObject);
				}
	}

}
