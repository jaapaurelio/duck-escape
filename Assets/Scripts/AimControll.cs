using UnityEngine;
using System.Collections;

public class AimControll : MonoBehaviour {

	static float LEVEL_TIME = 5.0f;	// segundos
	static float LEVEL_SPEED_INCREASE = 0.5f;
	static float INITIAL_SPEED = 5f;

	float speed = INITIAL_SPEED;
	float speedWithDuck = 8.5f;
	bool started = false;
	bool moving = false;
	float timeLeft = LEVEL_TIME;
	GameObject levelUpLabel;

	// TODO Analisar uma forma melhor de mostrar o level up
	float timeShowLevelUp = 1f;


	public void Start() {
		levelUpLabel = GameObject.Find( "LevelUpLabel" );
	}

	public void StartGame () {

		gameObject.SetActive( true );

		// Coloca o objecto na posição inicial correcta
		transform.position = new Vector3( 0.7f, 0f, 0 );

		started = true;
		moving = false;
		timeLeft = LEVEL_TIME;
		speed = INITIAL_SPEED;
		levelUpLabel.SetActive( false );
	}

	void Update() {

		// Ainda nao clicou no ecra
		if( !started ) {
			return;
		}

		// Mira ja esta a andar
		if( moving ) {
			updateLevelTimer();
			return;
		}


		// No primeiro click após iniciar a cena activa a mira
		if ( Input.GetMouseButtonDown (0) ) {
			moving = true;
			// Aplica a força para mover a mira
			Vector2 direction = new Vector2( 1, 1 ).normalized;
			rigidbody2D.AddForce( direction * speed );
		}
	}

	void updateLevelTimer() {
		
		timeLeft -= Time.deltaTime;
		timeShowLevelUp -= Time.deltaTime;

		if( timeLeft < 0) {
			timeLeft = LEVEL_TIME;
			levelUp();
		}

		if ( timeShowLevelUp < 0 ) {
			levelUpLabel.SetActive( false );
		}

	}

	void levelUp() {
		speed += LEVEL_SPEED_INCREASE;
		levelUpLabel.SetActive( true );
		timeShowLevelUp = 1f;
	}

	void OnTriggerEnter2D(Collider2D coll) {

		var colliderName = coll.gameObject.name;

		// Quando a mira bate na parede
		// TODO Utilizar uma tag em vez de nomes individuais
		if ( colliderName == "BorderTop" ||
		   colliderName == "BorderBottom" ||
		   colliderName == "BorderRight" ||
		   colliderName == "BorderLeft" ) {

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

		
		// A mira ao passar pelo pato altera a sua tragetoria para a direção do pato.
		} else if( colliderName == "Duck" ) {
		
	
			Vector3 target;

			target = coll.gameObject.transform.position;
			target.z = 0;
			
			Vector2 direction = ( target - gameObject.transform.position ).normalized;
			
			// Para o movimento para nao acumular forças
			gameObject.rigidbody2D.velocity = Vector2.zero;
			gameObject.rigidbody2D.angularVelocity = 0;
			
			rigidbody2D.AddForce( direction * speedWithDuck ); 

		}

	}

	public void Hide() {
		gameObject.SetActive( false );
		started = false;
		moving = false;
	}

}
