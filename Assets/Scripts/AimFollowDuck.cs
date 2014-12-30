using UnityEngine;
using System.Collections;

public class AimFollowDuck : MonoBehaviour {

	public AudioClip aimAlertSound;

	void OnTriggerEnter2D( Collider2D coll ) {

		if( isDuck( coll )  ) {

			if( aimAlertSound != null ) {
				AudioSource.PlayClipAtPoint( aimAlertSound, Vector3.zero );
			}

			gameObject.SendMessageUpwards( "DuckColision", coll );
		}

	}

	
	private bool isDuck( Collider2D coll ) {
		
		return coll.gameObject.name == "Duck";
		
	}

}
