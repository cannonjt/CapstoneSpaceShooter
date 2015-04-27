using UnityEngine;
using System.Collections;

public class ControlsMenu : MonoBehaviour {
	public GameObject mainMenuButton;

	public void menuButtonClicked(){

		Application.LoadLevel("mainMenu");
	}
}
