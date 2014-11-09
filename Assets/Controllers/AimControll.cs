using UnityEngine;
using System.Collections;

public class AimControll : MonoBehaviour {

	float speed = 5.5f;

	public void StartGame () {
		// Aplica a força para mover a mira
		Vector2 direction = new Vector2( 1, 1 ).normalized;
		rigidbody2D.AddForce( direction * speed );
	}


	void OnTriggerEnter2D(Collider2D coll) {

		var colliderName = coll.gameObject.name;

		float yForce = 0f;
		float xForce = 0f;
		float xOriginal = gameObject.rigidbody2D.velocity.x;
		float yOriginal = gameObject.rigidbody2D.velocity.y;
		float minRange = 0.2f;
		float maxRange = 3f;

		if( colliderName == "BorderTop" || colliderName == "BorderBottom" ) {

			// Ao tocar em cima aplica força apara baixo
			if( colliderName == "BorderTop" ) {
				yForce = -1f;
			
			// Tocar em baixo aplica força para cima
			} else {
				yForce = 1f;
			}

			xForce = Random.Range( minRange, maxRange );

			// Inverte a direção da força para continuar a andar na mesma direção
			if( xOriginal < 0 ) {
				xForce = -xForce;
			}

		}

		if( colliderName == "BorderRight" || colliderName == "BorderLeft" ) {
			
			
			// Ao tocar em cima aplica força apara baixo
			if( colliderName == "BorderRight" ) {
				xForce = -1f;
				
				// Tocar em baixo aplica força para cima
			} else {
				xForce = 1f;
			}
			
			yForce = Random.Range( minRange, maxRange );
			
			// Inverte a direção da força para continuar a andar na mesma direção
			if( yOriginal < 0 ) {
				yForce = -yForce;
			}
			
		}

		Vector2 direction = new Vector2( xForce, yForce ).normalized;
		
		// Para o movimento para nao acumular forças
		gameObject.rigidbody2D.velocity = Vector2.zero;
		gameObject.rigidbody2D.angularVelocity = 0;
		
		rigidbody2D.AddForce( direction * speed ); 

	}

}
