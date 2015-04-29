using UnityEngine;
using System.Collections;

public class HealthPickUp : PickUp {
	public float heal;

	public override void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			GameObject player = GameObject.FindGameObjectWithTag ("Player");
			PlayerProperties playerProperties = (PlayerProperties)player.GetComponent (typeof(PlayerProperties));
			if (playerProperties.getMax ())
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
		PlayerProperties playerProperties = (PlayerProperties)player.GetComponent (typeof(PlayerProperties));
		playerProperties.healFor(heal);
		
	}
}
