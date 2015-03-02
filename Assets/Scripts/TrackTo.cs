using UnityEngine;
using System.Collections;

public class TrackTo : MonoBehaviour {

	public float patrolSpeed = 1f;
	public float chaseSpeed = 3f;
	public float minDist = 10f;
	public Transform target;
	public float distance;


	void Update(){

		float howFar = getDistance();

		if(howFar <= minDist) {

			Vector3 track = target.position - transform.position;
			print(track);

			Quaternion wantDir = Quaternion.LookRotation( track, Vector3.up ); 
			Quaternion newRotation = Quaternion.RotateTowards(rigidbody.rotation, wantDir, 60*Time.deltaTime);

			rigidbody.MoveRotation (newRotation);
		}
	}

	float getDistance(){

		distance = Vector3.Distance (target.position, transform.position);

		return distance;

	}
}
