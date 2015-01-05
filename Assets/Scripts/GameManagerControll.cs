using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GoogleMobileAds.Api;

public class GameManagerControll : MonoBehaviour {

	public GoogleAnalyticsV3 googleAnalytics;
	public GameObject gameScene;
	public GameObject mainMenuScene;
	public GameObject gameOverScene;
	public GameObject screenFader;

	private BannerView smallBannerBottom;
	private InterstitialAd interstitialBanner;

	private int activeSceen = 0;
	
	private int numberOfGamesLeftToShowAd = GameConsts.NumberOfGamesToShowAds;

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

		CreatSmallBanner();

	}

	void CreatSmallBanner() {

		smallBannerBottom = new BannerView(
			GameConsts.AdIdBanner, AdSize.SmartBanner, AdPosition.Bottom );

		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder()
			.Build();
		
		// Load the banner with the request.
		smallBannerBottom.LoadAd(request);

	}

	void CreatFullScreenBanner() {

		interstitialBanner = new InterstitialAd( GameConsts.AdIdGameOver );
		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();

		// Load the interstitial with the request.
		interstitialBanner.LoadAd(request);

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

		smallBannerBottom.Show();

		// Prepara a publicidade para ser mostrada no game over
		numberOfGamesLeftToShowAd--;
		if( numberOfGamesLeftToShowAd == 0 ) {
			CreatFullScreenBanner();
		}
	}


	public void LoadGameOver() {
		HideAllScenes();
		gameOverSceneControll.ShowGameOver();
		googleAnalytics.LogScreen("Game Over");
		activeSceen = 2;

		smallBannerBottom.Hide();

		if( numberOfGamesLeftToShowAd == 0 ) {
			numberOfGamesLeftToShowAd = GameConsts.NumberOfGamesToShowAds;

			if (interstitialBanner.IsLoaded() ) {
				interstitialBanner.Show();
			}
		}

	}

}
