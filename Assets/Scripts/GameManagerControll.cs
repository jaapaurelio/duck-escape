using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class GameManagerControll : MonoBehaviour {

	public GoogleAnalyticsV3 googleAnalytics;
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

		googleAnalytics.LogEvent(new EventHitBuilder()
		                         .SetEventCategory("Game")
		                         .SetEventAction("Open Game"));

	}

	void OnApplicationQuit() {
		googleAnalytics.LogEvent(new EventHitBuilder()
		                         .SetEventCategory("Game")
		                         .SetEventAction("Close Game"));
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
		googleAnalytics.LogScreen("Main Menu");
	}

	public void StartGameScene() {
		HideAllScenes();
		sceneFadeInOut.ShowFader( gameSceneControll.StartGame );
		googleAnalytics.LogScreen("Game Scene");

	}


	public void LoadGameOver() {
		HideAllScenes();
		gameOverSceneControll.ShowGameOver();
		googleAnalytics.LogScreen("Game Over");
	}


}
