using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public virtual void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			doAction();
			Destroy (gameObject);
		}
	}

	public virtual void doAction()
	{
		print ("Power up picked up");
	}
}
