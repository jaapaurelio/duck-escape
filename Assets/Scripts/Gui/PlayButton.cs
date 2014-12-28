using UnityEngine;
using System.Collections;

public class PlayButton : Button {

	public override void OnButtonClick() {

		GameObject gameManager = GameObject.Find( "GameManager" );

		GameManagerControll gameManagerControll = gameManager.GetComponent<GameManagerControll>();

		gameManagerControll.StartGameScene();

	}

}
