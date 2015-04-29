using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RestartLevel : MonoBehaviour {
	public GameObject pauseText;

	public bool isPaused;
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.K) == true) {
			Application.LoadLevel("Level1");
		}

		if (Input.GetKeyDown (KeyCode.B) == true) {
			Application.LoadLevel ("BossDemo");
		}

		if (Input.GetKeyDown (KeyCode.Escape) == true) 
		{
			if (Time.timeScale == 1)
			{
				isPaused = true;
				Image blue = GameObject.Find("Blueness").GetComponent<Image>();
				Color c = blue.color;
				c.a = .5f;
				blue.color = c;
				pauseText.SetActive(true);
				Time.timeScale = 0;
			}
			else
			{
				isPaused = false;
				Image blue = GameObject.Find("Blueness").GetComponent<Image>();
				Color c = blue.color;
				c.a = 0;
				blue.color = c;
				pauseText.SetActive(false);
				Time.timeScale = 1;
			}
		}

	}
}
