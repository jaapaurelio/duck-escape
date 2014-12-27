using UnityEngine;
using System.Collections;

public class AimControll : MonoBehaviour {

	float speed = GameConsts.AimInitialSpeed;
	bool started = false;
	bool moving = false;
	
	public void StartGame () {
		 
		gameObject.SetActive( true );

		// Coloca o objecto na posição inicial correcta
		transform.position = new Vector3( 0f, -1.7f, 0 );

		started = true;
		moving = false;
		speed = GameConsts.AimInitialSpeed;
	}

	void Update() {

		// Ainda nao clicou no ecra
		if( !started ) {
			return;
		}

		// Mira ja esta a andar
		if( moving ) {
			return;
		}

		// No primeiro click após iniciar a cena activa a mira
		if ( Input.GetMouseButtonDown (0) ) {
			StartCoroutine( StartMoving() );
		}
	}

	public IEnumerator StartMoving() {

		moving = true;

		yield return new WaitForSeconds( GameConsts.TimeBeforeAim );

		// Aplica a força para mover a mira
		Vector2 direction = new Vector2( 1, 1 ).normalized;
		rigidbody2D.velocity = direction * speed;

	}

	public void LevelUp() {
		speed += GameConsts.AimLevelSpeedIncreaser;
	}


	void OnTriggerEnter2D( Collider2D coll ) {

		if ( isWallColision( coll ) ) {
			WallColision( coll );

		} else if( isDuck( coll )  ) {
			DuckColision( coll );
		}

	}


	private bool isDuck( Collider2D coll ) {

		return coll.gameObject.name == "Duck";

	}


	private void DuckColision( Collider2D duck ) {
		Vector3 target;
		
		target = duck.gameObject.transform.position;
		target.z = 0;
		
		Vector2 direction = ( target - gameObject.transform.position ).normalized;
				
		rigidbody2D.velocity = direction * ( speed + GameConsts.AimFollowingDuckIncreaser );

		//PrepareToShoot();

	}
	

	private bool isWallColision( Collider2D coll ) {
		var colliderName = coll.gameObject.name;

		// TODO Utilizar uma tag em vez de nomes individuais
		return colliderName == "BorderTop" ||
		    	colliderName == "BorderBottom" ||
		    	colliderName == "BorderRight" ||
				colliderName == "BorderLeft";

	}


	private void WallColision( Collider2D wall ) {
		var colliderName = wall.gameObject.name;
		
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

		rigidbody2D.velocity = direction * speed; 
		

	}

	public void Shoot() {

		started = false;
		moving = false;

		gameObject.SetActive( false );
	}


}
