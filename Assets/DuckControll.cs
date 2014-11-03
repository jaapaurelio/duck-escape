 using UnityEngine;
using System.Collections;

public class DuckControll : MonoBehaviour {

	bool started = false;

	// Use this for initialization
	void Start () {

	}

	public void StartGame () {
		rigidbody2D.AddForce( new Vector2( -20, -20 ) );

		started = true;

	}


	// Update is called once per frame
	void Update () {

		if( !started ) {

			if ( Input.GetMouseButtonDown (0) ) {

				GameObject aim = GameObject.Find( "Aim" );
				AimControll aimControll = aim.GetComponent<AimControll>();
				aimControll.StartGame();

				GameObject timer = GameObject.Find( "Timer" );
				TimerControll timerControll = timer.GetComponent<TimerControll>();
				timerControll.StartGame();

				started = true;
			}
		}

		if( !started ) {
			return;
		}

		GameObject player = GameObject.Find( "Pato" );
		Vector3 target;

		if ( Input.GetMouseButtonDown (0) ) {
			
			Vector3 mouseClick = Input.mousePosition;
			mouseClick.z = player.transform.position.z - Camera.main.transform.position.z;
			target = Camera.main.ScreenToWorldPoint (mouseClick);

			target.z = 0;
			
			Vector2 direction = ( target - player.transform.position).normalized;
			
			
			player.rigidbody2D.velocity = Vector2.zero;
			player.rigidbody2D.angularVelocity = 0;
			
			player.rigidbody2D.AddForce ( direction * 30 );

		}
	}


	void OnCollisionEnter2D(Collision2D coll) {
		if ( coll.gameObject.name == "Aim")
			Application.LoadLevel( "Game" );
		
	}

}
