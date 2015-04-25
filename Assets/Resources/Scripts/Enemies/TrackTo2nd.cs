using UnityEngine;
using System.Collections;

public class TrackTo2nd : MonoBehaviour {
	
	public float patrolSpeed;
	public float chaseSpeed;       //max movement speed of the enemy while chasing the player
	public float thrustSpeed;      //acceleration of the enemy
	public float minDist;          //distance to the player where trusting will stop  
	public float maxDist;          //farthest away entity can be and still chase the target
	public float shootDist;        //distance to the target to start shooting
	public Transform target;       //what the entity is following around
	public float distance;         //distance between entity and the target
	public Vector3 startPosition;  //Starting location of the enemy
	
	public float fieldOfViewAngle = 110f;  // Number of degrees, centred on forward, for the enemy see.
	public float lengthOfSight = 2f;            // Distance in front of the entity that it can "see"
	public bool objectDetected = false;	   // True if the entity "sees" an object and needs to change course
	public float obstacleAngle;
	
	
	void Start(){
		target = GameObject.FindGameObjectWithTag("Player").transform;
		StartCoroutine(setSpawn());
	}
	
	IEnumerator setSpawn(){
		yield return new WaitForSeconds(0.75f);
		startPosition = transform.position;
	}
	
	void Update(){
		if (target != null) {
			
			float howFar = getDistance ();
			
			if (howFar <= maxDist) {
				
				//rotates the enemy ship to the player
				//from: http://answers.unity3d.com/questions/207505/following-ai-similar-to-snake-game.html
				Vector3 track = target.position - transform.position;
				
				Quaternion wantDir = Quaternion.LookRotation (track, Vector3.up); 
				Quaternion newRotation = Quaternion.RotateTowards (rigidbody.rotation, wantDir, 240 * Time.deltaTime);
				
				
				//look in the direction of the player
				rigidbody.MoveRotation (newRotation);
				
				//moves to the player 
				//add thrust in the direction of movement
				if (rigidbody.velocity.magnitude < chaseSpeed && howFar >= minDist) {
					checkSight ();

					//object avoidance 
					if (objectDetected == true) { //if you detect an asteroid rotate away from it.
						
						transform.Rotate (0, (-obstacleAngle * 3.0f), 0);
						rigidbody.AddForce (transform.forward * thrustSpeed);
						
					} else {					  //if you do not detect and asteroid then move forward
						Vector3 IC = CalculateInterceptCourse(target.position, target.rigidbody.velocity, transform.position, chaseSpeed);
						rigidbody.AddForce (IC * thrustSpeed);
					}
					
				} else { rigidbody.angularVelocity = Vector3.zero; }
				
			} else { 
				Vector3 goHome = startPosition - transform.position;
				
				Quaternion wantDir = Quaternion.LookRotation (goHome, Vector3.up); 
				Quaternion newRotation2 = Quaternion.RotateTowards (rigidbody.rotation, wantDir, 60 * Time.deltaTime);
				
				rigidbody.MoveRotation (newRotation2);
				if (rigidbody.velocity.magnitude < patrolSpeed){
					rigidbody.AddForce (transform.forward * thrustSpeed);
				}
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

	//Gives a Vector that will hit the target if they do not change speed or direction
	//Note: The bullet must move fast enough or it will simply return a vector pointing straight at
	//the target.
	//Adapted from Bunny83's post at: http://answers.unity3d.com/questions/296949/how-to-calculate-a-position-to-fire-at.html
	public static Vector3 CalculateInterceptCourse(Vector3 aTargetPos, Vector3 aTargetSpeed, Vector3 aInterceptorPos, float aInterceptorSpeed)
	{
		Vector3 targetDir = aTargetPos - aInterceptorPos; //vector to the player ship
		
		float iSpeed2 = aInterceptorSpeed * aInterceptorSpeed; //bullet speed squared (two floats)
		float tSpeed2 = aTargetSpeed.sqrMagnitude; //ship speed squared (two vector3s)
		
		float fDot1 = Vector3.Dot(targetDir, aTargetSpeed); //larger if crossing path is parallel
		
		float targetDist2 = targetDir.sqrMagnitude; //from bullet to ship squared
		
		//D is smaller when more angular adjustment is required
		float d = (fDot1 * fDot1) //D will be higher if targetDir and targetSpeed are more parallel 
			- targetDist2 //D will be higher if distance is greater (as long as bullet is fast)
				* (tSpeed2 - iSpeed2); //will be negative if bullet is faster than target
		
		if (d < 0.1f) {  // negative == no possible course because the interceptor isn't fast enough
			return targetDir; //return a path straight to the player
			
		}
		
		float sqrt = Mathf.Sqrt(d);
		
		float S1 = (-fDot1 - sqrt) / targetDist2; //first scalar adjustment
		float S2 = (-fDot1 + sqrt) / targetDist2; //secondary scalar adjustment
		
		
		//pick the vector with larger adjustment multiplication
		//apply it to vector to player + vector of player movement
		if (S1 < 0.0001f)
		{
			if (S2 < 0.0001f)
			{
				//shoot directly at the player
				return targetDir;
				
			}
			else
				return (S2) * targetDir + aTargetSpeed;
		}
		else if (S2 < 0.0001f)
			return (S1) * targetDir + aTargetSpeed;
		else if (S1 < S2)
			return (S2) * targetDir + aTargetSpeed;
		else
			return (S1) * targetDir + aTargetSpeed;
	}
}
