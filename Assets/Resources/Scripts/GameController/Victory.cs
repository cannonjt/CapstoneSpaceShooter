using UnityEngine;
using System.Collections;

public class Victory : MonoBehaviour {

	public void victory()
	{
		StartCoroutine (loadVictory ());
	}

	IEnumerator loadVictory(){
		yield return new WaitForSeconds(5f);
		if (GameObject.FindGameObjectsWithTag("Player").Length > 0)
			Application.LoadLevel ("VictoryMenu");
	}
}
