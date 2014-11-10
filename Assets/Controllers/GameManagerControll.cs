using UnityEngine;
using System.Collections;

public class GameManagerControll : MonoBehaviour {
	
	GameObject gameScene;
	GameObject mainMenuScene;
	GameObject aim;
	GameObject timer;
	GameObject duck;
	GameObject gameOverScene;
	GameObject screenFader;
	AimControll aimControll;
	TimerControll timerControll;
	DuckControll duckControll;
	SceneFadeInOut sceneFadeInOut;

	// Use this for initialization
	void Start () {

		aim = GameObject.Find( "Aim" );
		timer = GameObject.Find( "Timer" );
		duck = GameObject.Find( "Duck" );

		
		gameScene = GameObject.Find( "GameScene" );
		mainMenuScene = GameObject.Find( "MainMenuScene" );
		screenFader = GameObject.Find( "ScreenFader" );
		gameOverScene = GameObject.Find( "GameOverScene" );

		aimControll = aim.GetComponent<AimControll>();
		timerControll = timer.GetComponent<TimerControll>();
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
		timerControll.StartGame();

	}

	public void GameOver() {
		HideAllScenes();

		gameOverScene.SetActive( true );

	}
	
	// Update is called once per frame
	void Update () {

	}
}
