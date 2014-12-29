using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;

public class GameManager : GooglePlayGames.BasicApi.OnStateLoadedListener {
	private static GameManager sInstance = new GameManager();
	private GameProgress mProgress;

	private bool mAuthenticating = false;

	private bool isBestScore = false;

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
		Debug.Log("Cloud load callback, success=" + success);
		if (success) {
			ProcessCloudData(data);
		} else {
			Debug.LogWarning("Failed to load from cloud. Network problems?");
		}
		
		// regardless of success, this is the end of the auth process
		mAuthenticating = false;
		
		// report any progress we have to report
		ReportAllProgress();
	}
	
	// Conflict in cloud data occurred
	public byte[] OnStateConflict(int slot, byte[] local, byte[] server) {
		Debug.Log("Conflict callback called. Resolving conflict.");
		
		// decode byte arrays into game progress and merge them
		GameProgress localProgress = local == null ?
			new GameProgress() : GameProgress.FromBytes(local);
		GameProgress serverProgress = server == null ?
			new GameProgress() : GameProgress.FromBytes(server);
		localProgress.MergeWith(serverProgress);
		
		// resolve conflict
		return localProgress.ToBytes();
	}
	
	public void OnStateSaved(bool success, int slot) {
		if (!success) {
			Debug.LogWarning("Failed to save state to the cloud.");
			
			// try to save later:
			mProgress.Dirty = true;
		}
	}

	void ProcessCloudData(byte[] cloudData) {
		if (cloudData == null) {
			Debug.Log("No data saved to the cloud yet...");
			return;
		}
		Debug.Log("Decoding cloud data from bytes.");
		GameProgress progress = GameProgress.FromBytes(cloudData);
		Debug.Log("Merging with existing game progress.");
		mProgress.MergeWith(progress);
	}

	void ReportAllProgress() {
		/*FlushAchievements();
		UnlockProgressBasedAchievements();
		PostToLeaderboard();
		SaveToCloud();*/
	}


	public void PostToLeaderboard( int score ) {

		if (Authenticated && score > mHighestPostedScore) {
			// post score to the leaderboard
			Social.ReportScore(score, GameIds.LeaderboardId, (bool success) => {});
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
				// if we signed in successfully, load data from cloud

				// TODO Carregar dados guardados
				//LoadFromCloud();
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

	public bool Authenticated {
		get {
			return Social.Active.localUser.authenticated;
		}
	}

	public void EndLevel( int score ) {
		isBestScore = mProgress.SetScore( score );

		if( mProgress.Dirty ) {
			mProgress.SaveToDisk();
		}

		PostToLeaderboard( score );
	}

	public GameProgress Progress {
		get {
			return mProgress;
		}
	}

	public bool IsBestScore {
		get {
			return isBestScore;
		}
	}

	public void ShowLeaderboardUI() {
		if (Authenticated) {
			Social.ShowLeaderboardUI();
		} else {
			Authenticate();
		}
	}
	
}
