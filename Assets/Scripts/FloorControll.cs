using UnityEngine;
using System.Collections;

public class FloorControll : MonoBehaviour {
	
	GameSceneControll gameSceneControll;
	
	void Awake ()
	{
		gameSceneControll = GameObject.Find( "GameScene" ).GetComponent<GameSceneControll>();
	}

	void OnTriggerEnter2D(Collider2D coll) {

		Debug.Log("bareu");
		if ( coll.gameObject.name == "Duck" ) {
			gameSceneControll.GameOver();
		}
	}
}
