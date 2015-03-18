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
	
	public float fieldOfViewAngle = 110f;  // Number of degrees, centred on forward, for the enemy see.
	public float lengthOfSight = 2f;            // Distance in front of the entity that it can "see"
	public bool objectDetected = false;	   // True if the entity "sees" an object and needs to change course
	public float obstacleAngle;

	void Start(){
		
	}
	
	void Update(){
		if (target != null) {

			float howFar = getDistance ();

			if (howFar <= maxDist) {

				//rotates the enemy ship to the player
				//from: http://answers.unity3d.com/questions/207505/following-ai-similar-to-snake-game.html
				Vector3 track = target.position - transform.position;

				Quaternion wantDir = Quaternion.LookRotation (track, Vector3.up); 
				Quaternion newRotation = Quaternion.RotateTowards (rigidbody.rotation, wantDir, 60 * Time.deltaTime);

				//look in the direction of the player
				rigidbody.MoveRotation (newRotation);

				//moves to the player 
				//add thrust in the direction of movement
				if (rigidbody.velocity.magnitude < chaseSpeed && howFar >= minDist) {
					checkSight ();
					if (objectDetected == true) {
							//avoid running in to it.
							//Vector3 Angle = new Vector3(-obstacleAngle* 4.0f ,0, System.Math.Abs(obstacleAngle));
							//Vector3 Where = Angle - transform.position; 
							//Quaternion wantDirObstacle = Quaternion.LookRotation(Where, Vector3.up ); 
							//Quaternion newRotationObstacle = Quaternion.RotateTowards(rigidbody.rotation, wantDirObstacle, 60*Time.deltaTime);
							//rigidbody.MoveRotation (newRotationObstacle);
							//transform.Rotate(0,-obstacleAngle* 3.0f,0);
							transform.Rotate (0, (-obstacleAngle * 3.0f), 0);
							rigidbody.AddForce (transform.forward * thrustSpeed);
							//print ("eek" + obstacleAngle);
					} else {
						rigidbody.AddForce (transform.forward * thrustSpeed);
					}
				} else { rigidbody.angularVelocity = Vector3.zero; }
			}
		}
	}
	
	//Calculates the distance between the target and its self
	float getDistance(){
		
		distance = Vector3.Distance (target.position, transform.position);
		return distance;
	}
	
	//sight
	void checkSight() {
		GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Asteroid");
		
		//find closest
		GameObject closestA = null;
		float distance2 = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject go in obstacles) {
				Vector3 diff = go.transform.position - position;
				float curDistance = diff.sqrMagnitude;
				if (curDistance < distance2) {
										closestA = go;
										distance2 = curDistance;
				}
		}
		objectDetected = false;

		//can we see it
		if(closestA != null){
			if ((Vector3.Distance (closestA.transform.position ,transform.position)) <= lengthOfSight) { 
			
				// Create a vector from the enemy to the asteroid and store the angle between it and forward.
				Vector3 direction = closestA.transform.position - transform.position;
				obstacleAngle = Vector3.Angle (direction, transform.forward);
				Vector3 cross = Vector3.Cross (direction, transform.forward);
				
				// If the angle between forward and where the player is, is less than half the angle of view...
				if (obstacleAngle < fieldOfViewAngle * 0.5f) {
					objectDetected = true;
					if(cross.y < 0){ obstacleAngle = -obstacleAngle; }
				}
				else{
					objectDetected = false;
				}	
			}
		}
	}
}
