using UnityEngine;
using System.Collections;

public class mainMenuController : MonoBehaviour {
	public GameObject playButton;
	public GameObject quitButton;
	public GameObject infoButton;
	public GameObject loadingText;

	public void playButtonClicked(){
		loadingText.SetActive (true);
		Application.LoadLevel("Level1");
	}
	public void quitButtonClicked(){
		Application.Quit ();
	}

	public void infoButtonClicked(){
		Application.LoadLevel("ControlsMenu");
	}
}
