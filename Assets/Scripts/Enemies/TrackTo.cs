using UnityEngine;
using System.Collections;

public class TrackTo : MonoBehaviour {

	public float patrolSpeed = 1f;
	public float chaseSpeed = 3f;
	public float minDist = 10f;
	public float shootDist;
	public Transform target;
	public float distance;

	void Start(){
		if (target == null) {
			target = FindClosestEnemy().transform;
		}
	}

	void Update(){

		float howFar = getDistance();

		if(howFar <= minDist) {

			Vector3 track = target.position - transform.position;

			Quaternion wantDir = Quaternion.LookRotation( track, Vector3.up ); 
			Quaternion newRotation = Quaternion.RotateTowards(rigidbody.rotation, wantDir, 60*Time.deltaTime);

			rigidbody.MoveRotation (newRotation);
		}
	}

	float getDistance(){

		distance = Vector3.Distance (target.position, transform.position);

		return distance;

	}

	//Targets the closest enemy if no specified target
	//from http://docs.unity3d.com/ScriptReference/GameObject.FindGameObjectsWithTag.html
	GameObject FindClosestEnemy() {
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject go in gos) {
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance) {
				closest = go;
				distance = curDistance;
			}
		}
		return closest;
	}
}
