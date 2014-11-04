using UnityEngine;
using System.Collections;

public class GameControll : MonoBehaviour {

	bool started = false;
	GameObject aim;
	GameObject timer;
	GameObject duck;
	AimControll aimControll;
	TimerControll timerControll;
	DuckControll duckControll;

	// Use this for initialization
	void Start () {

		aim = GameObject.Find( "Aim" );
		timer = GameObject.Find( "Timer" );
		duck = GameObject.Find( "Duck" );

		aimControll = aim.GetComponent<AimControll>();
		timerControll = timer.GetComponent<TimerControll>();
		duckControll = duck.GetComponent<DuckControll>();

	}
	
	// Update is called once per frame
	void Update () {

		if( !started ) {
			// No primeiro toque começa o jogo
			if ( Input.GetMouseButtonDown (0) ) {
				duckControll.StartGame();
				aimControll.StartGame();
				timerControll.StartGame();
				started = true;
			}
		}
	}
}
