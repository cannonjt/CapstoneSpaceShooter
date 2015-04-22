using UnityEngine;
using System.Collections;

public class mainMenuController : MonoBehaviour {
	public GameObject playButton;
	public GameObject quitButton;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void playButtonClicked(){
		Application.LoadLevel("Level1");
	}
	public void quitButtonClicked(){
		Application.Quit ();
	}
}
