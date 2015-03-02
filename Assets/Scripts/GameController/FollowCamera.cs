using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {
	private GameObject camera;

	// Use this for initialization
	void Start () {
		camera = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 cameraPOS = camera.transform.transform.position;

		transform.position = new Vector3(cameraPOS.x, 0, cameraPOS.z);
	}
}
