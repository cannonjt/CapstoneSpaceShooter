using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMax, xMin, zMax, zMin;
}

public class PlayerController : MonoBehaviour {
	
	public Boundary boundary;
	public float tilt;
	public float turnSpeed;
	public float thrustSpeed;
	public float maxSpeed;
	public Weapon currentWep;

	private GameObject thruster;

	void Start()
	{
		thruster = GameObject.Find ("engines_player");
		currentWep = (Weapon)Instantiate (currentWep);
		currentWep.GetComponent<Weapon> ().setUp (gameObject);
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
		currentWep.shoot();
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

	public void changeWeapon(Weapon newWep)
	{
		Destroy(currentWep.gameObject);
		currentWep = (Weapon)Instantiate (newWep);
		currentWep.GetComponent<Weapon> ().setUp (gameObject);

	}




}
