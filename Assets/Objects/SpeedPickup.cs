using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpeedPickup : PickUp {

	public float duration;

	public override void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			GameObject player = GameObject.FindGameObjectWithTag ("Player");
			PlayerController playerC = (PlayerController)player.GetComponent (typeof(PlayerController));
			if (playerC.getBoost())
			{
				//do nothing
			}
			else
			{
				doAction ();
				Destroy (gameObject);
			}
		}
	}
	public override void doAction()
	{
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		PlayerController playerController = (PlayerController)player.GetComponent (typeof(PlayerController));
		playerController.speedBoost(true, duration);
		
	}
}