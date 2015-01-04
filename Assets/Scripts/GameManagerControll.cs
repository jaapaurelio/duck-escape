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

	private int activeSceen = 0;

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

		StartCoroutine( LoginPlayServices() );

	}

	public IEnumerator LoginPlayServices() {

		yield return new WaitForSeconds( GameConsts.TimeToFirstPlayServicesLogin );

		GameManager.Instance.Authenticate();

	}

	void Update(){

		// Ao clicar no botão Back do telemovel
		if ( Input.GetKeyDown( KeyCode.Escape ) )  {

			// Game Over
			if( activeSceen == 2 ) {
				StartMainMenuScene();

			// Main menu
			} else if ( activeSceen == 0 ) {
				Application.Quit();
			}

		}
	}

	void OnApplicationQuit() {

		googleAnalytics.LogEvent(new EventHitBuilder()
		                         .SetEventCategory("Game")
		                         .SetEventAction("Close Game"));
	}

	public void OnApplicationPause() {
		GameManager.Instance.ShowingLeaderboard = false;
		Debug.Log( "JAAP OnApplicationPause " );
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
		activeSceen = 0;
	}

	public void StartGameScene() {
		HideAllScenes();
		sceneFadeInOut.ShowFader( gameSceneControll.StartGame );
		googleAnalytics.LogScreen("Game Scene");
		activeSceen = 1;
	}


	public void LoadGameOver() {
		HideAllScenes();
		gameOverSceneControll.ShowGameOver();
		googleAnalytics.LogScreen("Game Over");
		activeSceen = 2;
	}


}
