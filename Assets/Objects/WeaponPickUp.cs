using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WeaponPickUp : PickUp {

	public Weapon containedWep;
	public Sprite icon;

	public override void doAction()
	{
		Image bulletIcon = GameObject.Find("BulletIcon").GetComponent<Image>();
		bulletIcon.sprite = icon;
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		PlayerController playerController = (PlayerController)player.GetComponent (typeof(PlayerController));
		playerController.changeWeapon (containedWep);

	}
}
