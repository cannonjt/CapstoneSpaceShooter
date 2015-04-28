using UnityEngine;
using System.Collections;

public class RestartLevel : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.K) == true) {
			Application.LoadLevel("Level1");
		}

		if (Input.GetKeyDown (KeyCode.B) == true) {
			Application.LoadLevel ("BossDemo");
		}
	}
}
