using UnityEngine;
using System.Collections;

public class TrackTo : MonoBehaviour {

	public float patrolSpeed;
	public float chaseSpeed;       //max movement speed of the enemy while chasing the player
	public float thrustSpeed;      //acceleration of the enemy
	public float minDist;          //distance to the player where trusting will stop  
	public float maxDist;          //farthest away entity can be and still chase the target
	public float shootDist;        //distance to the target to start shooting
	public Transform target;       //what the entity is following around
	public float distance;         //distance between entity and the target

	void Start(){

	}

	void Update(){

		float howFar = getDistance();

		if(howFar <= maxDist) {

			//rotates the enemy ship to the player
			//from: http://answers.unity3d.com/questions/207505/following-ai-similar-to-snake-game.html
			Vector3 track = target.position - transform.position;

			Quaternion wantDir = Quaternion.LookRotation( track, Vector3.up ); 
			Quaternion newRotation = Quaternion.RotateTowards(rigidbody.rotation, wantDir, 60*Time.deltaTime);

			//look in the direction of the player
			rigidbody.MoveRotation (newRotation);

			//moves to the player 
			//add thrust in the direction of movement
			if (rigidbody.velocity.magnitude < chaseSpeed && howFar >= minDist) {
					
				rigidbody.AddForce (transform.forward * thrustSpeed);
			}
		}
	}

	//Calculates the distance between the target and its self
	float getDistance(){

		distance = Vector3.Distance (target.position, transform.position);
		return distance;
	}


}
