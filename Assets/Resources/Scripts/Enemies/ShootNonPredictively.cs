using UnityEngine;
using System.Collections;

public class ShootNonPredictively : MonoBehaviour {
	
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
		if (Input.GetKeyDown (KeyCode.Q) == true) {
			doooIt ();
		}
	}
	
	public void doooIt()
	{
		GameObject g = GameObject.Find ("GameController");
		PredictiveLoaders b = g.GetComponent<PredictiveLoaders>();
		b.dooIt ();
		Destroy (this.gameObject);
	}
	//Gives a Vector that will hit the target if they do not change speed or direction
	//Note: The bullet must move fast enough or it will simply return a vector pointing straight at
	//the target.
	//Adapted from Bunny83's post at: http://answers.unity3d.com/questions/296949/how-to-calculate-a-position-to-fire-at.html
	public static Vector3 CalculateInterceptCourse(Vector3 aTargetPos, Vector3 aTargetSpeed, Vector3 aInterceptorPos, float aInterceptorSpeed)
	{
		Vector3 targetDir = aTargetPos - aInterceptorPos; //vector to the player ship
		//print ("TargetDir: " + targetDir);
		return targetDir;
	}
}
