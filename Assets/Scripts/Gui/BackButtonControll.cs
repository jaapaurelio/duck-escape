using UnityEngine;
using System.Collections;

public class BackButtonControll : Button {

	public override void OnButtonClick() {
		GameObject gameManager = GameObject.Find( "GameManager" );
		
		GameManagerControll gameManagerControll = gameManager.GetComponent<GameManagerControll>();
		
		gameManagerControll.StartMainMenuScene();

	}
}
