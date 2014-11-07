using UnityEngine;
using System.Collections;

public class AimControll : MonoBehaviour {

	int speed = 30;

	public void StartGame () {
		// Aplica a força para mover a mira
		Vector2 direction = new Vector2( 1, 1 ).normalized;
		rigidbody2D.AddForce( direction * speed );
	}


	void OnTriggerEnter2D(Collider2D coll) {

		var colliderName = coll.gameObject.name;

		Debug.Log( colliderName );
		if( colliderName == "BorderTop" || colliderName == "BorderBottom" ) {

			float directionY = gameObject.rigidbody2D.velocity.y;

			float range = -Random.Range( directionY - directionY, directionY + directionY );
			Vector2 direction = new Vector2( gameObject.rigidbody2D.velocity.x, range ).normalized;

			// Para o movimento para nao acumular forças
			gameObject.rigidbody2D.velocity = Vector2.zero;
			gameObject.rigidbody2D.angularVelocity = 0;

			rigidbody2D.AddForce( direction * speed );


		}

		if( colliderName == "BorderRight" || colliderName == "BorderLeft" ) {
			
			float originalDirection = gameObject.rigidbody2D.velocity.x;
			
			float range = -Random.Range( originalDirection - originalDirection, originalDirection + originalDirection );

			Vector2 direction = new Vector2( range,  gameObject.rigidbody2D.velocity.y ).normalized;
			
			// Para o movimento para nao acumular forças
			gameObject.rigidbody2D.velocity = Vector2.zero;
			gameObject.rigidbody2D.angularVelocity = 0;
			
			rigidbody2D.AddForce( direction * speed );
			
			
		}

	}

}
