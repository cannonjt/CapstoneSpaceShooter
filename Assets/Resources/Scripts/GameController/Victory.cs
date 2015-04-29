using UnityEngine;
using System.Collections;

public class Victory : MonoBehaviour {

	public void victory()
	{
		StartCoroutine (loadVictory ());
	}

	IEnumerator loadVictory(){
		yield return new WaitForSeconds(5f);
		Application.LoadLevel ("VictoryMenu");
	}
}
