using UnityEngine;
using System.Collections;

[System.Serializable]
public class moveSettings{
	public float chaseSpeed;       //max movement speed of the follower while chasing
	public float thrustSpeed;      //acceleration of the follower
	public float minDist;          //distance from the leader where thrusting will stop  
	public float maxRange;          //farthest away entity can be from the leader
	public float shootDist;        //distance to the target to start shooting
	public float rotationSpeed;


}

public class FollowerBehavior : MonoBehaviour {
	
	public moveSettings move;
	public Transform leader;       //what the follower is following around
	public Weapon currentWep;

	private Transform target;		//what the follower wants to shoot at
	private float distance;         //distance between entity and the target
	private bool returning;			//if the follower is returning to its leader
	private bool friendly;

	// Use this for initialization
	void Start () {
		returning = true;
		currentWep = (Weapon)Instantiate (currentWep);
		currentWep.GetComponent<Weapon> ().setUp (gameObject);
		currentWep.transform.parent = transform;

		if (leader.tag == "Player") {
				friendly = true;
		} else
				friendly = false;

	}
	
	// Update is called once per frame
	void Update () {

		if (leader != null) {

				float howFarFromLeader = getDistance (leader);


				//seek a target if not returning to leader and not too far away
				if (returning == false && howFarFromLeader <= move.maxRange) {

						GameObject tempEnemy;
						if (friendly) {
								tempEnemy = FindClosestEnemy ();
						} else
								tempEnemy = GameObject.FindGameObjectWithTag ("Player");

						if (tempEnemy != null) {
								target = tempEnemy.transform;
								float howFarFromEnemy = getDistance (target);

								if (howFarFromEnemy < move.maxRange) {
										//enemy is within the max range
										moveTowards (target);

										if (howFarFromEnemy <= move.shootDist) {

												if (friendly)
														rigidbody.velocity = Vector3.zero;
												//shoot at enemy
												currentWep.shoot ();
										}
								} else
										returnHome ();
						} else
								returnHome ();
				} else { //ship is returning to leader
						returning = true;
						returnHome ();
				}
		} else
		{
			//follower is offline
			GameObject aExplosion =(GameObject)Resources.Load("Prefabs/VFX/Explosions/explosion_enemy");
			Instantiate (aExplosion, this.transform.position, this.transform.rotation);
			Destroy(this.gameObject);
		}
				
	}

	//commands the follower to return to the player
	void returnHome(){

		float leaderDist = Vector3.Distance (leader.position, transform.position);

		if (leaderDist >= 0.75f * move.maxRange) {
				//move towards leader
				moveTowards (leader);
		}

		else {
			returning = false;
		}
	}

	//Targets the closest enemy
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

	//Calculates the distance between the target and its self
	float getDistance(Transform otherTrans){
		
		distance = Vector3.Distance (otherTrans.position, transform.position);
		return distance;
	}

	void moveTowards(Transform targetTrans)
	{
		float howFar = getDistance(targetTrans);

		//rotates the enemy ship to the player
		//from: http://answers.unity3d.com/questions/207505/following-ai-similar-to-snake-game.html
		Vector3 track = targetTrans.position - transform.position;
		
		Quaternion wantDir = Quaternion.LookRotation( track, Vector3.up ); 
		Quaternion newRotation = Quaternion.RotateTowards(rigidbody.rotation, wantDir, 
		                                                  move.rotationSpeed*Time.deltaTime);
		
		//look in the direction of the player
		rigidbody.MoveRotation (newRotation);
		
		//moves to the player 
		//add thrust in the direction of movement
		if (rigidbody.velocity.magnitude < move.chaseSpeed && howFar >= move.minDist) {
			
			rigidbody.AddForce (transform.forward * move.thrustSpeed);
		}
	}
	
}
