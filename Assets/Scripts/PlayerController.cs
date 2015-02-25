using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMax, xMin, zMax, zMin;
}

public class PlayerController : MonoBehaviour {

	public float speed;
	public Boundary boundary;
	public float tilt;
	public float rotationSpeed;
	public float moveHorizontal;
	public float moveVertical;
	public float turnSpeed;
	public float thrustSpeed;
	public float maxSpeed;

	void FixedUpdate()
	{
		moveHorizontal = Input.GetAxis ("Horizontal");
		//goes between -1 and 1 (-1 is left)
		moveVertical = Input.GetAxis ("Vertical");

		//clamp the ship in a specific region
		rigidbody.position = new Vector3
			(
				Mathf.Clamp (rigidbody.position.x, boundary.xMin, boundary.xMax),
				0.0f,
				Mathf.Clamp (rigidbody.position.z, boundary.zMin, boundary.zMax)
			);

		if (moveHorizontal != 0f || moveVertical != 0f) {
			Rotate(moveHorizontal, moveVertical);	
			//add thrust in the direction of movement
			if(rigidbody.velocity.magnitude < maxSpeed){

				rigidbody.AddForce(transform.forward * thrustSpeed);

			}
		}
		//rigidbody.rotation = Quaternion.Euler (0.0f, 0.0f, rigidbody.velocity.x * -tilt);


		//rigidbody.rotation = Quaternion.Euler (0.0f, 

	}

	void Rotate(float horiz, float vert)
	{
		//create targetDirection
		Vector3 targetDirection = new Vector3 (horiz, 0f, vert);

		//create a quaternion rotation based on vector (independent of camera rotation)
		//rotating around the y axis
		Quaternion targetRotation = Quaternion.LookRotation (targetDirection, Vector3.up);

		//Create rotation increment between current and target rotation
		Quaternion newRotation = Quaternion.Lerp (rigidbody.rotation, targetRotation, turnSpeed * Time.deltaTime);

		//change the player rotation to reflect
		rigidbody.MoveRotation (newRotation);
	}


}
