using UnityEngine;
using System.Collections;

public class BackButtonControll : MonoBehaviour {

	void OnMouseDown () {
		GameObject gameManager = GameObject.Find( "GameManager" );
		
		GameManagerControll gameManagerControll = gameManager.GetComponent<GameManagerControll>();
		
		gameManagerControll.StartMainMenuScene();

	}
}
