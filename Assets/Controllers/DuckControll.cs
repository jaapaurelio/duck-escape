 using UnityEngine;
using System.Collections;

public class DuckControll : MonoBehaviour {

	bool started = false;

	// Use this for initialization
	void Start () {

	}
	
	public void StartGame () {

		GameObject aim = GameObject.Find( "Aim" );
		GameObject timer = GameObject.Find( "Timer" );
		
		AimControll aimControll = aim.GetComponent<AimControll>();
		TimerControll timerControll = timer.GetComponent<TimerControll>();
		
		aimControll.StartGame();
		timerControll.StartGame();

		started = true;
	}


	// Update is called once per frame
	void Update () {

		GameObject player = GameObject.Find( "Pato" );
		Vector3 target;

		if( !started ) {

			// No primeiro toque começa o jogo
			if ( Input.GetMouseButtonDown (0) ) {
				StartGame();
			}
		}

		// Ainda nao clicou no ecra
		if( !started ) {
			return;
		}

		if ( Input.GetMouseButtonDown (0) ) {
			
			Vector3 mouseClick = Input.mousePosition;
			mouseClick.z = player.transform.position.z - Camera.main.transform.position.z;

			target = Camera.main.ScreenToWorldPoint( mouseClick );
			target.z = 0;
			
			Vector2 direction = ( target - player.transform.position ).normalized;

			// O objecto é parado para nao acumular
			player.rigidbody2D.velocity = Vector2.zero;
			player.rigidbody2D.angularVelocity = 0;

			// Aplica a força ao objecto na direcao do clique
			player.rigidbody2D.AddForce ( direction * 30 );

		}
	}


	void OnCollisionEnter2D(Collision2D coll) {

		// Quando o jogador bate na mira o jogo reinicia
		if ( coll.gameObject.name == "Aim") {
			Application.LoadLevel( "Game" );
		}
		
	}

}
