using UnityEngine;
using System.Collections;

public class ShootPredictively : MonoBehaviour {

	public Rigidbody target;
	public Weapon currentWep;
	private float speed;
	public Transform turretShotSpawn;


	// Use this for initialization
	void Start () {
		currentWep = (Weapon)Instantiate (currentWep);
		currentWep.GetComponent<Weapon> ().setUp (gameObject);
		if (turretShotSpawn != null)
						currentWep.GetComponent<Weapon> ().setSpawnLocation (turretShotSpawn);
		speed = currentWep.GetComponent<Weapon> ().shotProperties.shot.GetComponent<Mover> ().speed;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 IC = CalculateInterceptCourse(target.position, target.velocity, transform.position, speed);

		if (!IC.Equals (Vector3.zero)) {
			//create a quaternion rotation based on vector (independent of camera rotation)
			//rotating around the y axis
			Quaternion targetRotation = Quaternion.LookRotation (IC, Vector3.up);

			//change the shooter's rotation to reflect
			//rigidbody.MoveRotation (targetRotation);
			transform.rotation = targetRotation;

			//Transform shotSpawn = transform.GetChild(0);
			//shotSpawn.rotation = Quaternion.AngleAxis((transform.rotation.y + Mathf.PingPong(Time.time, 90) - 45f), Vector3.up);
			//shotSpawn.rotation = Quaternion.Euler (new Vector3(transform.rotation.x, (transform.rotation.y +
			                                 //Mathf.PingPong(Time.time, 120) - 120f), transform.rotation.z));
			//http://answers.unity3d.com/questions/321323/how-to-give-your-enemy-gun-inaccuracy.html

			currentWep.shoot ();
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
