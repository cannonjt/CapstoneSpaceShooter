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
		if (target == null) {
			target = FindClosestEnemy().transform; /**TODO: Also needs to be in a place that is can refind a target if 
			                                        target == null.**/
		}
	}

	void Update(){

		if(gameObject.tag == "Follower"){
			returnHome ();
		}
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

	void returnHome(){

		Transform parent = GameObject.FindGameObjectWithTag("Player").transform;
		float playerDist = Vector3.Distance (parent.position, transform.position);

		if (playerDist >= maxDist) {  /**TODO: make a new variable for follower.... Also we need away to reset the target back to an enemy once he is closer to the player **/
						target = parent.transform;	
		} 
		//else {
			//target = FindClosestEnemy().transform;
		//}
	}
}
