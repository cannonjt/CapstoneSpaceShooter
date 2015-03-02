using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
	void OnTriggerEnter(Collider other) 
	{
			Destroy(gameObject);

	}

}
