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
	
	ScoreControll scoreControll;
	SceneFadeInOut sceneFadeInOut;
	GameSceneControll gameSceneControll;

	// Use this for initialization
	void Start () {

		// Activate the Google Play Games platform
		PlayGamesPlatform.Activate();
		
		gameScene = GameObject.Find( "GameScene" );
		mainMenuScene = GameObject.Find( "MainMenuScene" );
		gameOverScene = GameObject.Find( "GameOverScene" );

	
		scoreControll = GameObject.Find( "Score" ).GetComponent<ScoreControll>();
		sceneFadeInOut = GameObject.Find( "ScreenFader" ).GetComponent<SceneFadeInOut>();
		gameSceneControll = gameScene.GetComponent<GameSceneControll>();

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

		sceneFadeInOut.ShowFader( gameSceneControll.StartGame );

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


}
