using UnityEngine;
using System.Collections;

public class MinimapFollow : MonoBehaviour {
	
	private GameObject player;
	
	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 PlayerPOS = player.transform.transform.position;
		GameObject.Find("MinimapCamera").transform.position = new Vector3(PlayerPOS.x, 10, PlayerPOS.z);
		
	}
}