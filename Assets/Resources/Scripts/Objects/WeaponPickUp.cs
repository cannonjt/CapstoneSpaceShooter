using UnityEngine;
using System.Collections;

public class WeaponPickUp : PickUp {

	public Weapon containedWep;

	public override void doAction()
	{
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		PlayerController playerController = (PlayerController)player.GetComponent (typeof(PlayerController));
		playerController.changeWeapon (containedWep);

	}
}
