using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMax, xMin, zMax, zMin;
}
[System.Serializable]
public class ShotProperties
{
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;

	[HideInInspector]
	public float nextFire;


}

public class PlayerController : MonoBehaviour {
	
	public Boundary boundary;
	public ShotProperties shotProperties;
	public float tilt;
	public float turnSpeed;
	public float thrustSpeed;
	public float maxSpeed;

	private GameObject thruster;

	void Start()
	{
		thruster = GameObject.Find ("engines_player");
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		//goes between -1 and 1 (-1 is left)
		float moveVertical = Input.GetAxis ("Vertical");
		float horizontalHeld = Input.GetAxisRaw ("Horizontal");
		float verticalHeld = Input.GetAxisRaw ("Vertical");


		//clamp the ship in a specific region
		rigidbody.position = new Vector3
			(
				Mathf.Clamp (rigidbody.position.x, boundary.xMin, boundary.xMax),
				0.0f,
				Mathf.Clamp (rigidbody.position.z, boundary.zMin, boundary.zMax)
			);

		if (horizontalHeld != 0f || verticalHeld != 0f) {
			//movement keys held, calculate rotations and turn on thruster
			Rotate (moveHorizontal, moveVertical);
			thruster.SetActive (true);


			//add thrust in the direction of movement
			if (rigidbody.velocity.magnitude < maxSpeed) {

					rigidbody.AddForce (transform.forward * thrustSpeed);

			}
		} else {
			//turn off thruster if not moving
			thruster.SetActive (false);
		}

	}

	void Update()
	{
		shoot();
	}

	void Rotate(float horiz, float vert)
	{
		//create targetDirection
		Vector3 targetDirection = new Vector3 (horiz, 0f, vert);

		//ensure it is not attempting to rotate when not needed
		if( !targetDirection.Equals(Vector3.zero)){

			//create a quaternion rotation based on vector (independent of camera rotation)
			//rotating around the y axis
			Quaternion targetRotation = Quaternion.LookRotation (targetDirection, Vector3.up);
			
			//Create rotation increment between current and target rotation
			Quaternion newRotation = Quaternion.RotateTowards (rigidbody.rotation, targetRotation, turnSpeed * Time.deltaTime);

			//change the player rotation to reflect
			rigidbody.MoveRotation (newRotation);
		}

	}

	void shoot()
	{
		if (Input.GetButton ("Fire1") && Time.time > shotProperties.nextFire) {
			shotProperties.nextFire = Time.time + shotProperties.fireRate;
			Instantiate(shotProperties.shot, shotProperties.shotSpawn.position,
			            shotProperties.shotSpawn.rotation);
			audio.Play ();	
		}
	}


}
