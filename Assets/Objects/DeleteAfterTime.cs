using UnityEngine;
using System.Collections;

public class DeleteAfterTime : MonoBehaviour {
	public float timeToLive;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, timeToLive);
	}
}
