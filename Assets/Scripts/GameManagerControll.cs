using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class GameManagerControll : MonoBehaviour {
	
	GameObject gameScene;
	GameObject mainMenuScene;
	GameObject gameOverScene;
	GameObject screenFader;

	SceneFadeInOut sceneFadeInOut;
	GameSceneControll gameSceneControll;
	GameOverSceneControll gameOverSceneControll;

	// Use this for initialization
	void Start () {

		
		gameScene = GameObject.Find( "GameScene" );
		mainMenuScene = GameObject.Find( "MainMenuScene" );
		gameOverScene = GameObject.Find( "GameOverScene" );

		sceneFadeInOut = GameObject.Find( "ScreenFader" ).GetComponent<SceneFadeInOut>();
		gameSceneControll = gameScene.GetComponent<GameSceneControll>();
		gameOverSceneControll = gameOverScene.GetComponent<GameOverSceneControll>();

		SaveControll.control.Load();

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

		sceneFadeInOut.ShowFader( gameSceneControll.StartGame );

	}


	public void LoadGameOver() {
		HideAllScenes();
		gameOverSceneControll.ShowGameOver();
	}


}
