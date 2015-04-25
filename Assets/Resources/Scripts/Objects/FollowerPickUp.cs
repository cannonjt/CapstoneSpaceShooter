using UnityEngine;
using System.Collections;

public class FollowerPickUp : PickUp {

	
	public override void doAction()
	{
		//Finds the player that is being followed
		GameObject player = GameObject.FindGameObjectWithTag ("Player");

		//Creates the Follower, position of follower
		GameObject spawnFollower = (GameObject)Resources.Load ("Prefabs/Player/Follower");
		Vector3 spawnPos = player.transform.position;
		GameObject newFollower = (GameObject)Instantiate (spawnFollower, spawnPos, Quaternion.identity);

		//set the leader of follower
		FollowerBehavior fBeh = newFollower.GetComponent<FollowerBehavior> ();
		fBeh.leader = player.transform;
	}
}
