using UnityEngine;
using System.Collections;

public class FloorControll : MonoBehaviour {

	GameObject gameManager;
	GameManagerControll gameManagerControll;

	void Start() {
		gameManager = GameObject.Find( "GameManager" );
		gameManagerControll = gameManager.GetComponent<GameManagerControll>();
	}

	void OnTriggerEnter2D(Collider2D coll) {

		if ( coll.gameObject.name == "Duck" ) {
			gameManagerControll.GameOver();
		}
	}
}
