using UnityEngine;
using System.Collections;

public class PlayerOutOfBounds : MonoBehaviour {

	public float maxPlayerDist;
	public float warningDist;
	public GameObject warningText;
	public Transform boundaryCircle;

	private GameObject player;
	private ShootPredictively shootScript;
	private float audioVol;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		shootScript = gameObject.GetComponent<ShootPredictively>();
		shootScript.enabled = false;
		audioVol = 0f;

		//set up the boundary circle and its transparency
		if (boundaryCircle != null) {
			boundaryCircle.transform.localScale = new Vector3(warningDist * 2f, warningDist * 2f, 1f);
			Color transColor = boundaryCircle.renderer.material.color;
			transColor.a = 0.25f;
			boundaryCircle.renderer.material.color = transColor;
		}


	}
	
	// Update is called once per frame
	void Update () {
		float currentDist = getDistanceToPlayer ();

		if (currentDist > maxPlayerDist) {
			shootScript.enabled = true;
			warningText.SetActive(true);

			resetVol();
			if(!audio.isPlaying) audio.Play();
		}
		else if(currentDist > warningDist)
		{
			warningText.SetActive(true);

			resetVol();
			if(!audio.isPlaying) audio.Play();
		}
		else{
			shootScript.enabled = false;
			warningText.SetActive(false);
			fadeAudio();
		}
	}

	float getDistanceToPlayer()
	{
		float distance = Vector3.Distance (player.transform.position, transform.position);
		return distance;

	}

	void fadeAudio(){

		if (audioVol > 0.25f) {
			audioVol -= 0.2f * Time.deltaTime;
			audio.volume = audioVol;
		} else {
			audio.Stop ();
			resetVol ();
		}
	}

	void resetVol()
	{
		audioVol = 1f;
		audio.volume = audioVol;
	}
}
		