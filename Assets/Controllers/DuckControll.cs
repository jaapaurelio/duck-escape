 using UnityEngine;
using System.Collections;

public class DuckControll : MonoBehaviour {

	bool started = false;
	float speed = 5;
	
	public void StartGame () {

		// Coloca o objecto na posição inicial correcta
		transform.position = new Vector3( -0.9f, -0.5f, 0 );

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

		}
	}


	void OnTriggerEnter2D(Collider2D coll) {


		GameObject gameManager = GameObject.Find( "GameManager" );
		
		GameManagerControll gameManagerControll = gameManager.GetComponent<GameManagerControll>();


		// Quando o jogador bate na mira o jogo reinicia
		if ( coll.gameObject.name == "Aim" ) {
			gameManagerControll.StartGameScene();
		}

		// 12 - Borders
		// TODO Utilizar nome da layer em vez do Id
		if( coll.gameObject.layer == 12 ) {
			gameManagerControll.StartGameScene();
		}
		
	}

}
