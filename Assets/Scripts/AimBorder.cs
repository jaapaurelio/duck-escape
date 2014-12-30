using UnityEngine;
using System.Collections;

public class AimBorder : MonoBehaviour {

	void OnTriggerEnter2D( Collider2D coll ) {

		if ( isWallColision( coll ) ) {
			gameObject.SendMessageUpwards("WallColision", coll );
		}

	}

	private bool isWallColision( Collider2D coll ) {
		
		return coll.gameObject.tag == "Border";
		
	}
}
