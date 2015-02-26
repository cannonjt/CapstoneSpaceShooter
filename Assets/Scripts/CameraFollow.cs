using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 PlayerPOS = player.transform.transform.position;
		GameObject.Find("Main Camera").transform.position = new Vector3(PlayerPOS.x, 10, PlayerPOS.z);

	}
}
