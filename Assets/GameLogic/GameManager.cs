using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;

public class GameManager : GooglePlayGames.BasicApi.OnStateLoadedListener {
	private static GameManager sInstance = new GameManager();
	private GameProgress mProgress;

	private bool mAuthenticating = false;

	private bool isSoundActive = true;

	private bool showingLeaderboard = false;

	// what is the highest score we have posted to the leaderboard?
	private int mHighestPostedScore = 0;

	public static GameManager Instance {
		get {
			return sInstance;
		}
	}
	
	private GameManager() {
		mProgress = GameProgress.LoadFromDisk();

		// Enable/disable logs on the PlayGamesPlatform
		PlayGamesPlatform.DebugLogEnabled = GameConsts.PlayGamesDebugLogsEnabled;
		
		// Activate the Play Games platform. This will make it the default
		// implementation of Social.Active
		PlayGamesPlatform.Activate();
		
		// Set the default leaderboard for the leaderboards UI
		((PlayGamesPlatform) Social.Active).SetDefaultLeaderboardForUI( GameIds.LeaderboardId );

	}

	// Data was successfully loaded from the cloud
	public void OnStateLoaded(bool success, int slot, byte[] data) {
		// TODO Será utilizado quando guardar online
	}
	
	// Conflict in cloud data occurred
	public byte[] OnStateConflict(int slot, byte[] local, byte[] server) {

		// TODO Será utilizado quando guardar online
		return null;
	}
	
	public void OnStateSaved(bool success, int slot) {
		// TODO Será utilizado quando guardar online
	}


	public void PostToLeaderboard( int score ) {

		if (Authenticated && score > mHighestPostedScore) {

			// post score to the leaderboard
			Social.ReportScore(score, GameIds.LeaderboardId, (bool success) => {
				if( success ) {
					mProgress.Synchronized = true;
				}
			});

			mHighestPostedScore = score;
		}
	}

	#if UNITY_ANDROID || UNITY_IOS
	public void Authenticate() {

		if (Authenticated || mAuthenticating) {
			Debug.LogWarning("Ignoring repeated call to Authenticate().");
			return;
		}
		
		// Sign in to Google Play Games
		mAuthenticating = true;
		Social.localUser.Authenticate((bool success) => {
			mAuthenticating = false;
			if (success) {
				updateLeaderboardScore();
			} else {
				// no need to show error message (error messages are shown automatically
				// by plugin)
				Debug.LogWarning("Failed to sign in with Google Play Games.");
			}
		});
	}
	#else
	public void Authenticate() {
		mAuthenticating = false;
	}
	#endif

	private void updateLeaderboardScore(){

		if( !mProgress.Synchronized ) {
			PostToLeaderboard( mProgress.BestScore );
		}

	}

	public bool Authenticated {
		get {
			return Social.Active.localUser.authenticated;
		}
	}

	public void EndLevel( int score ) {
		mProgress.NewScore( score );
		mProgress.SaveLocal();
		PostToLeaderboard( score );
	}

	public GameProgress Progress {
		get {
			return mProgress;
		}
	}


	public bool IsSoundActive {
		get {
			return isSoundActive;
		}
		set {
			isSoundActive = value;
		}
	}

	public bool ShowingLeaderboard {
		set {
			showingLeaderboard = value;
		}
	}

	
	public void ShowLeaderboardUI() {

		if (Authenticated) {
			if ( !showingLeaderboard ) {
				showingLeaderboard = true;
				Social.ShowLeaderboardUI();
			}
		} else {
			Authenticate();
		}

	}
	
}
