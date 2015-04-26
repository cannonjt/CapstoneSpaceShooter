using UnityEngine;
using System.Collections;

public class DestroyAll : MonoBehaviour {

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag != "Player" && other.gameObject.tag != "KillerProjectile"){
			Destroy (other.gameObject);
		}
	}
}
