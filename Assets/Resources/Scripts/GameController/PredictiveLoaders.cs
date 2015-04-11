using UnityEngine;
using System.Collections;

public class PredictiveLoaders : MonoBehaviour {
	public bool swapLoaders;
	public GameObject p;

	void Start(){
		swapLoaders = false;
	}

	void Update () {
		if (swapLoaders) {
			p.SetActive(true);
			swapLoaders = false;
				}
		if (Input.GetKeyDown (KeyCode.K) == true) {
			Application.LoadLevel("Predictive_Demo");
		}
		}

	public void dooIt()
	{
		swapLoaders = true;
	}
}
