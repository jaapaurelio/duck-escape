using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class GameManagerControll : MonoBehaviour {
	
	GameObject gameScene;
	GameObject mainMenuScene;
	GameObject gameOverScene;
	GameObject screenFader;
	GameObject borders;
	GameObject floors;

	AimControll aimControll;
	ScoreControll scoreControll;
	DuckControll duckControll;
	ShootControll shootControll;
	SceneFadeInOut sceneFadeInOut;

	// Use this for initialization
	void Start () {

		// Activate the Google Play Games platform
		PlayGamesPlatform.Activate();
		
		gameScene = GameObject.Find( "GameScene" );
		mainMenuScene = GameObject.Find( "MainMenuScene" );
		gameOverScene = GameObject.Find( "GameOverScene" );

		borders = GameObject.Find( "Borders" );
		floors = GameObject.Find( "Floors" );
	
		aimControll = GameObject.Find( "Aim" ).GetComponent<AimControll>();
		scoreControll = GameObject.Find( "Score" ).GetComponent<ScoreControll>();
		duckControll = GameObject.Find( "Duck" ).GetComponent<DuckControll>();
		sceneFadeInOut = GameObject.Find( "ScreenFader" ).GetComponent<SceneFadeInOut>();
		shootControll = GameObject.Find( "Shoot" ).GetComponent<ShootControll>();

		SaveControll.control.Load();


		// authenticate user:
		Social.localUser.Authenticate((bool success) => {
			// handle success or failure
			if( success ){
				//Debug.Log("login com sucesso");				
			}else{
				//Debug.Log("login insucesso");
			}
			
		});		
		
		StartMainMenuScene();

	}

	void HideAllScenes() {
		gameScene.SetActive( false );
		mainMenuScene.SetActive( false );
		gameOverScene.SetActive( false );
	}

	public void StartMainMenuScene() {
		HideAllScenes();
		mainMenuScene.SetActive( true );
		mainMenuScene.transform.position = new Vector3(0, 0, -10);
	}

	public void StartGameScene() {
		HideAllScenes();

		sceneFadeInOut.ShowFader( StartGame );

	}

	public void StartGame() {
		gameScene.SetActive( true );
		gameScene.transform.position = new Vector3(0, 0, -10);
		duckControll.StartGame();
		aimControll.StartGame();
		scoreControll.Reset();
		scoreControll.StartGame();

		ShowBorders();
	}

	public void Shoot() {

		shootControll.Shoot();

		ShowFloor();
		
		scoreControll.Stop();
		SaveControll.control.Save();
		aimControll.Hide();
		duckControll.Shoot();

		//gameOverScene.SetActive( true );

	}

	public void GameOver() {
		HideAllScenes();
		gameOverScene.SetActive( true );
		gameOverScene.transform.position = new Vector3(0, 0, -10);

		scoreControll.SetHighScore();
		scoreControll.addScore();

		// TODO mover para controlador do ecra game over
		GameObject highScore = GameObject.Find( "HighScore" );
		GameObject finalScore = GameObject.Find( "FinalScore" );
		highScore.GetComponent<Text>().text = scoreControll.getBestScore().ToString();
		finalScore.GetComponent<Text>().text = scoreControll.GetScore().ToString();

	}

	void ShowBorders() {
		// Coloca as borders para o pato morrer
		borders.SetActive( true );
		floors.SetActive( false );
	}

	void ShowFloor() {
		// Coloca o chao para o pato bater e saltar
		borders.SetActive( false );
		floors.SetActive( true );
	}
}
