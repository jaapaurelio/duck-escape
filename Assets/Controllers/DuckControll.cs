 using UnityEngine;
using System.Collections;

public class DuckControll : MonoBehaviour {

	bool started = false;
	float speed = 5;
	GameObject gameHelp;

	void Start(){
		gameHelp = GameObject.Find( "GameHelp" );
	}

	public void StartGame () {

		// Coloca o objecto na posição inicial correcta
		transform.position = new Vector3( -1f, -0.5f, 0 );
		transform.localEulerAngles = new Vector3( 0, 0, 5 );

		gameHelp.SetActive( true );

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

			// esconde a ajuda
			// TODO arranjar forma de nao fazer isto sempre que clicamos
			gameHelp.SetActive( false );

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


	void OnTriggerEnter2D(Collider2D coll) {


		GameObject gameManager = GameObject.Find( "GameManager" );
		
		GameManagerControll gameManagerControll = gameManager.GetComponent<GameManagerControll>();


		// Quando o jogador bate na mira o jogo reinicia
		if ( coll.gameObject.name == "Aim" ) {
			gameManagerControll.GameOver();
		}

		// 12 - Borders
		// TODO Utilizar nome da layer em vez do Id
		if( coll.gameObject.layer == 12 ) {
			gameManagerControll.GameOver();
		}
		
	}

}
