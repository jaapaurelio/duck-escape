using UnityEngine;
using System.Collections;

public class GameSceneControll : MonoBehaviour {

	public GoogleAnalyticsV3 googleAnalytics;

	private GameObject tutorial;
	GameObject borders;
	GameObject floors;

	AimControll aimControll;
	ScoreControll scoreControll;
	DuckControll duckControll;
	GameManagerControll gameManagerControll;


	void Start() {
		tutorial = GameObject.Find( "Tutorial" );
		borders = GameObject.Find( "Borders" );
		floors = GameObject.Find( "Floors" );

		aimControll = GameObject.Find( "Aim" ).GetComponent<AimControll>();
		scoreControll = GameObject.Find( "Score" ).GetComponent<ScoreControll>();
		duckControll = GameObject.Find( "Duck" ).GetComponent<DuckControll>();
		gameManagerControll = GameObject.Find( "GameManager" ).GetComponent<GameManagerControll>();

	}

	public void LevelUp() {
		aimControll.LevelUp();
		duckControll.LevelUp();
	}

	public void StartGame() {

		gameObject.SetActive( true );
		transform.position = new Vector3(0, 0, -10);

		tutorial.SetActive( true );

		duckControll.StartGame();
		aimControll.StartGame();
		scoreControll.Reset();
		scoreControll.StartGame();
		
		ShowBorders();

	}

	
	void ShowBorders() {
		// Coloca as borders para o pato morrer
		borders.SetActive( true );
		floors.SetActive( false );
	}


	void Update() {

		if ( Input.GetMouseButtonDown (0) ) {
			tutorial.SetActive( false );
		}
	}

	
	public void EndGame( int typeOfDeath ) {
	
		ShowFloor();
		scoreControll.Stop();
		aimControll.Shoot();
		duckControll.Shoot();
		GameManager.Instance.EndLevel( scoreControll.GetScore() );

		if ( typeOfDeath == GameConsts.TypeOfDeathAim ) {
			googleAnalytics.LogEvent(new EventHitBuilder()
			                     .SetEventCategory("Game Over")
		                         .SetEventAction("Aim"));
		} else {
			googleAnalytics.LogEvent(new EventHitBuilder()
			                     .SetEventCategory("Game Over")
		                         .SetEventAction("Border"));
		}
	}


	void ShowFloor() {
		// Coloca o chao para o pato bater e saltar
		borders.SetActive( false );
		floors.SetActive( true );
	}


	public void GameOver() {
		gameManagerControll.LoadGameOver();

	}
}
