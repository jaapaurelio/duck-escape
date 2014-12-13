﻿ using UnityEngine;
using System.Collections;

public class DuckControll : MonoBehaviour {

	bool started = false;
	float speed = 5;
	
	public void StartGame () {

		// Coloca o objecto na posição inicial correcta
		transform.position = new Vector3( -1f, -0.5f, 0 );
		transform.localEulerAngles = new Vector3( 0, 0, 0 );

		rigidbody2D.gravityScale = 0;
		started = true;

	}
	
	// Update is called once per frame
	void Update () {

		Vector3 target;

		// Ainda nao clicou no ecra
		if( !started ) {
			return;
		}

		if ( Input.GetMouseButtonDown (0) ) {

			Vector3 mouseClick = Input.mousePosition;
			mouseClick.z = gameObject.transform.position.z - Camera.main.transform.position.z;

			target = Camera.main.ScreenToWorldPoint( mouseClick );
			target.z = 0;
			
			Vector2 direction = ( target - gameObject.transform.position ).normalized;

			// O objecto é parado para nao acumular
			gameObject.rigidbody2D.velocity = Vector2.zero;
			gameObject.rigidbody2D.angularVelocity = 0;

			// Aplica a força ao objecto na direcao do clique
			gameObject.rigidbody2D.AddForce ( direction * speed );

			// Roda o pato dna direção do clique
			Vector3 duckPosition = gameObject.transform.position;
			int rotationY = 0;
			int rotationZ = 0;

			if( target.x <  duckPosition.x ) {
				rotationY = 180;
			}

			if ( target.y < duckPosition.y ) {
				rotationZ = -5;
			} else {
				rotationZ = 5;
			}

			transform.localEulerAngles = new Vector3( 0, rotationY, rotationZ );


		}
	}

	public void Shoot() {
		started = false;

		gameObject.rigidbody2D.AddTorque( 0.5f );

		rigidbody2D.gravityScale = 0.5f;
	}


	void OnTriggerEnter2D( Collider2D coll ) {
		
		GameSceneControll gameSceneControll = GameObject.Find( "GameScene" ).GetComponent<GameSceneControll>();

		// Quando o jogador bate na mira o jogo reinicia
		if ( coll.gameObject.name == "AimCenter" ) {
			gameSceneControll.EndGame();

		// 12 - Borders
		// TODO Utilizar nome da layer em vez do Id
		} else if( coll.gameObject.layer == 12 ) {
			gameSceneControll.EndGame();
		}
		
	}

}
