using UnityEngine;
using System.Collections;

public class GameSceneControll : MonoBehaviour {

	private GameObject gameHelp;
	GameObject borders;
	GameObject floors;

	AimControll aimControll;
	ScoreControll scoreControll;
	DuckControll duckControll;
	ShootControll shootControll;
	GameManagerControll gameManagerControll;


	void Start() {
		gameHelp = GameObject.Find( "GameHelp" );
		borders = GameObject.Find( "Borders" );
		floors = GameObject.Find( "Floors" );

		aimControll = GameObject.Find( "Aim" ).GetComponent<AimControll>();
		scoreControll = GameObject.Find( "Score" ).GetComponent<ScoreControll>();
		duckControll = GameObject.Find( "Duck" ).GetComponent<DuckControll>();
		shootControll = GameObject.Find( "Shoot" ).GetComponent<ShootControll>();
		gameManagerControll = GameObject.Find( "GameManager" ).GetComponent<GameManagerControll>();

	}


	public void StartGame() {

		gameObject.SetActive( true );
		transform.position = new Vector3(0, 0, -10);

		gameHelp.SetActive( true );

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
			gameHelp.SetActive( false );
		}
	}

	
	public void EndGame() {
		
		shootControll.Shoot();
		
		ShowFloor();
		
		scoreControll.Stop();
		aimControll.Hide();
		duckControll.Shoot();
		SaveControll.control.Save();
	}


	void ShowFloor() {
		// Coloca o chao para o pato bater e saltar
		borders.SetActive( false );
		floors.SetActive( true );
	}


	public void GameOver() {

		scoreControll.SetHighScore();
		scoreControll.addScore();
		gameManagerControll.LoadGameOver();

	}
}
