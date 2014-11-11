using UnityEngine;
using System.Collections;

public class GameManagerControll : MonoBehaviour {
	
	GameObject gameScene;
	GameObject mainMenuScene;
	GameObject aim;
	GameObject score;
	GameObject duck;
	GameObject gameOverScene;
	GameObject screenFader;
	AimControll aimControll;
	ScoreControll scoreControll;
	DuckControll duckControll;
	SceneFadeInOut sceneFadeInOut;

	// Use this for initialization
	void Start () {

		aim = GameObject.Find( "Aim" );
		score = GameObject.Find( "Score" );
		duck = GameObject.Find( "Duck" );

		
		gameScene = GameObject.Find( "GameScene" );
		mainMenuScene = GameObject.Find( "MainMenuScene" );
		screenFader = GameObject.Find( "ScreenFader" );
		gameOverScene = GameObject.Find( "GameOverScene" );

		aimControll = aim.GetComponent<AimControll>();
		scoreControll = score.GetComponent<ScoreControll>();
		duckControll = duck.GetComponent<DuckControll>();
		sceneFadeInOut = screenFader.GetComponent<SceneFadeInOut>();

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
	}

	public void StartGameScene() {
		HideAllScenes();

		sceneFadeInOut.ShowFader( StartGame );

	}

	public void StartGame() {
		gameScene.SetActive( true );
		duckControll.StartGame();
		aimControll.StartGame();
		scoreControll.Reset();
		scoreControll.StartGame();

	}

	public void GameOver() {
		HideAllScenes();

		scoreControll.Stop();

		gameOverScene.SetActive( true );

	}
	
	// Update is called once per frame
	void Update () {

	}
}
