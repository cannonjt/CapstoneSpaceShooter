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

	void FixedUpdate()
	{
		moveHorizontal = Input.GetAxis ("Horizontal");
		//goes between -1 and 1 (-1 is left)
		moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rigidbody.velocity = speed * movement;

		//float rotationalAngle = Vector3.Angle(new Vector3(0.0f,

		//goal: get rotation value between current vector and target vector

		rigidbody.position = new Vector3
			(
				Mathf.Clamp (rigidbody.position.x, boundary.xMin, boundary.xMax),
				0.0f,
				Mathf.Clamp (rigidbody.position.z, boundary.zMin, boundary.zMax)
			);

		//rigidbody.rotation = Quaternion.Euler (0.0f, 0.0f, rigidbody.velocity.x * -tilt);


		//rigidbody.rotation = Quaternion.Euler (0.0f, 

	}
}
