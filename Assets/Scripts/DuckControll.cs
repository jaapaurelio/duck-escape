 using UnityEngine;
using System.Collections;

public class DuckControll : MonoBehaviour {

	bool started = false;
	float speed = GameConsts.DuckInitialSpeed;
	Vector3 target;
	Vector2 direction;

	public void StartGame () {

		// Coloca o objecto na posição inicial correcta
		transform.position = new Vector3( -1f, -0.5f, 0 );
		transform.localEulerAngles = new Vector3( 0, 0, 0 );

		rigidbody2D.gravityScale = 0;
		started = true;

		speed = GameConsts.DuckInitialSpeed;

		target = transform.position;
		direction = Vector2.zero;
		
	}
	
	// Update is called once per frame
	void Update () {

		// Ainda nao clicou no ecra
		if( !started ) {
			return;
		}

		int i = 0;

		while (i < Input.touchCount ) {
			if (Input.GetTouch(i).phase == TouchPhase.Began) {
	
				var touch = Input.GetTouch(i);

				target = Camera.main.ScreenToWorldPoint( new Vector3(touch.position.x, touch.position.y, 0.0f ) );
				target.z = 0;
				
				direction = ( target - gameObject.transform.position ).normalized;

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
			i++;
		}

		if( Input.touchCount > 0 ) {
			// O objecto é parado para nao acumular
			gameObject.rigidbody2D.velocity = Vector2.zero;
			gameObject.rigidbody2D.angularVelocity = 0;
			
			// Aplica a força ao objecto na direcao do clique
			gameObject.rigidbody2D.AddForce ( direction * ( speed + GameConsts.DuckSpeedIncrease ) );
		} else {
			// O objecto é parado para nao acumular
			gameObject.rigidbody2D.velocity = Vector2.zero;
			gameObject.rigidbody2D.angularVelocity = 0;
			
			// Aplica a força ao objecto na direcao do clique
			gameObject.rigidbody2D.AddForce ( direction * speed );
		}
		
		
	}

	public void Shoot() {
		started = false;

		gameObject.rigidbody2D.AddTorque( 0.5f );

		rigidbody2D.gravityScale = 0.5f;
	}

	public void LevelUp() {
		speed += GameConsts.SpeedIncreaser;
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
