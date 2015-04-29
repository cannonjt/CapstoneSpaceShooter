using UnityEngine;
using System.Collections;

public class fixRotation : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.Euler (90, 0, 0);
	}
}
