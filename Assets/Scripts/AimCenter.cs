using UnityEngine;
using System.Collections;

public class AimCenter : MonoBehaviour {

	public AudioClip ShootSound;

	void OnTriggerEnter2D( Collider2D coll ) {

		if( coll.gameObject.tag == "KillDuck"  ) {

			AudioSource.PlayClipAtPoint( ShootSound, Vector3.zero );

			GameSceneControll gameSceneControll = GameObject.Find( "GameScene" ).GetComponent<GameSceneControll>();

			GameObject.Find( "Shoot" ).GetComponent<ShootControll>().Shoot();

			gameSceneControll.EndGame( GameConsts.TypeOfDeathAim );
			
		}
		
	}

}
