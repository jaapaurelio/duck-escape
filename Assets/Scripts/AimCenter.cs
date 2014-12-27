using UnityEngine;
using System.Collections;

public class AimCenter : MonoBehaviour {

	void OnTriggerEnter2D( Collider2D coll ) {

		if( coll.gameObject.tag == "KillDuck"  ) {

			GameSceneControll gameSceneControll = GameObject.Find( "GameScene" ).GetComponent<GameSceneControll>();

			GameObject.Find( "Shoot" ).GetComponent<ShootControll>().Shoot();

			gameSceneControll.EndGame();

			
		}
		
	}

}
