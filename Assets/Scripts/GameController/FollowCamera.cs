using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {
	private GameObject playerCamera;

	// Use this for initialization
	void Start () {
		playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 cameraPOS = playerCamera.transform.transform.position;

		transform.position = new Vector3(cameraPOS.x, 0, cameraPOS.z);
	}
}
