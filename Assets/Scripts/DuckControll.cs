 using UnityEngine;
using System.Collections;

public class DuckControll : MonoBehaviour {

	bool started = false;
	float speed = GameConsts.DuckInitialSpeed;
	Vector2 direction;

	public AudioClip TouchSound;
	public AudioClip HitWallSound;

	private Animator animator;    


	public void Start(){
		animator = GetComponent<Animator>();
	}

	public void StartGame () {

		// Coloca o objecto na posição inicial correcta
		transform.position = new Vector3( -1f, -0.5f, 0 );
		transform.localEulerAngles = new Vector3( 0, 0, 0 );

		GetComponent<Rigidbody2D>().gravityScale = 0;
		started = true;

		speed = GameConsts.DuckInitialSpeed;
		direction = Vector2.zero;

		animator.SetBool( "goFast", false );

	}


	void Update() {


		// Ainda nao clicou no ecra
		if( !started ) {
			return;
		}

		if( Input.touchCount > 0) {

			int i = 0;
			while ( i < Input.touchCount ) {
				
				// Verifica se é um dedo novo para mudar de direção
				if (Input.GetTouch(i).phase == TouchPhase.Began) {
					Vector3 target;
					var touch = Input.GetTouch(i);
					
					target = Camera.main.ScreenToWorldPoint( new Vector3(touch.position.x, touch.position.y, 0.0f ) );
					target.z = 0;
					
					direction = ( target - transform.position ).normalized;
					
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
					
					AudioSource.PlayClipAtPoint( TouchSound, Vector3.zero );
				
					animator.SetBool( "goFast", true );

				}
				
				i++;

			}
		
		// Não existe toque
		} else {
			animator.SetBool( "goFast", false );
		}

	}

	void FixedUpdate() {

		float applySpeed = speed;

		if ( !started ) {
			return;
		}

		// Aumenta velocidade quando utilizador deixa dedo no ecrã.
		if( Input.touchCount > 0 ) {
			applySpeed = speed + GameConsts.DuckSpeedIncrease;
		}

		GetComponent<Rigidbody2D>().velocity = direction * applySpeed;
	}

	public void Shoot() {

		started = false;
		GetComponent<Rigidbody2D>().AddTorque( 0.3f );
		GetComponent<Rigidbody2D>().gravityScale = 0.5f;
		animator.SetBool( "goFast", false );
	}


	public void LevelUp() {
		speed += GameConsts.DuckLevelSpeedIncrease;
	}


	void OnTriggerEnter2D( Collider2D coll ) {

		// 12 - Borders
		// TODO Utilizar nome da tag em vez do Id
		if( coll.gameObject.layer == 12 ) {

			AudioSource.PlayClipAtPoint( HitWallSound, Vector3.zero );

			GameSceneControll gameSceneControll = GameObject.Find( "GameScene" ).GetComponent<GameSceneControll>();
			GameObject.Find( "Shoot" ).GetComponent<ShootControll>().Shoot();

			gameSceneControll.EndGame( GameConsts.TypeOfDeathBorder );

		}
		
	}

}
