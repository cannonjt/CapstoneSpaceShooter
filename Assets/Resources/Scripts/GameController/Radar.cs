using UnityEngine;
using System.Collections;

public class Radar : MonoBehaviour {
	
	private GameObject player;
	private Transform helpTransform;
	public GameObject radarIcon;
	public float switchDistance;
	private Hashtable currentEnemies;
	private Hashtable borderObjects;
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		helpTransform = player.transform.GetChild (4);
		currentEnemies = new Hashtable ();
		borderObjects = new Hashtable ();
	}
	
	// Update is called once per frame
	void Update () {
		//Grab all of the enemy objects from the current enemies Hashtable and put it in a collection
		ICollection currentEnemyCollection = currentEnemies.Values;

		//Check to see if the player object still
		if (player != null) {
			//Iterate through each of the enemy objects in the current collection
			foreach (GameObject currentObject in currentEnemyCollection) {
				//If the current object is outside the render distance
				if (Vector3.Distance (currentObject.transform.position, helpTransform.position) > switchDistance) {
					//Takes the icon currently being displayed and moves it to the invisible layer
					currentObject.GetComponent<EnemyProperties> ().getIconChild ().layer = LayerMask.NameToLayer ("Invisible");

					//Takes the help transform of the player object and rotates it to face the current object
					helpTransform.LookAt (currentObject.transform.position);

					//Finds the current objects id number
					int IDnumber = currentObject.GetComponent<EnemyProperties> ().getEnemyID ();

					//Finds the current objects counterpart in the border objects
					GameObject currentBorderObject = (GameObject)borderObjects [IDnumber];

					//Adjusts the position of the current border object to show in which direction the current object lies
					currentBorderObject.transform.position = player.transform.position + switchDistance * helpTransform.forward;

					//Lock the rotation of the border object
					currentBorderObject.transform.rotation = Quaternion.Euler (90, currentObject.transform.rotation.y, currentObject.transform.rotation.z);

					//Makes the current border object visible on the radar
					currentBorderObject.layer = LayerMask.NameToLayer ("Radar");
				} else {
					//Changes the current object icon to the radar layar
					currentObject.GetComponent<EnemyProperties> ().getIconChild ().layer = LayerMask.NameToLayer ("Radar");
				
					//Changes the current border object to the invisible layer
					int IDnumber = currentObject.GetComponent<EnemyProperties> ().getEnemyID ();
					GameObject currentBorderObject = (GameObject)borderObjects [IDnumber];
					currentBorderObject.layer = LayerMask.NameToLayer ("Invisible");
				}
			}
		}
	}

	//
	public void addEnemy(int enemyID, GameObject enemy){
		currentEnemies.Add (enemyID, enemy);

		borderObjects.Add (enemyID, Instantiate(radarIcon, enemy.transform.position, enemy.transform.rotation));
	}
	
	public void removeEnemy(int enemyID)
	{
		currentEnemies.Remove (enemyID);
		GameObject deleteIt = (GameObject)borderObjects [enemyID];
		Destroy (deleteIt);
		borderObjects.Remove (enemyID);
	}
}