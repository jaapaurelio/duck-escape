using UnityEngine;
using System.Collections;

public class FloorControll : MonoBehaviour {
	
	GameSceneControll gameSceneControll;

	void Start() {
		gameSceneControll = GameObject.Find( "GameScene" ).GetComponent<GameSceneControll>();
	}

	void OnTriggerEnter2D(Collider2D coll) {

		if ( coll.gameObject.name == "Duck" ) {
			gameSceneControll.GameOver();
		}
	}
}
