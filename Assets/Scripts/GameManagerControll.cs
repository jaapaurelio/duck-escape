using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class GameManagerControll : MonoBehaviour {
	
	public GameObject gameScene;
	public GameObject mainMenuScene;
	public GameObject gameOverScene;
	public GameObject screenFader;

	SceneFadeInOut sceneFadeInOut;
	GameSceneControll gameSceneControll;
	GameOverSceneControll gameOverSceneControll;

	// Use this for initialization
	void Start () {

		sceneFadeInOut = screenFader.GetComponent<SceneFadeInOut>();
		gameSceneControll = gameScene.GetComponent<GameSceneControll>();
		gameOverSceneControll = gameOverScene.GetComponent<GameOverSceneControll>();

		StartMainMenuScene();

		GameManager.Instance.Authenticate();
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
