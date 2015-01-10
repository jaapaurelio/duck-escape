using System;
using UnityEngine;

public class GameProgress {
	private const string PlayerPrefsKey = "duckescape-game-progress";

	private int bestScore = 0;
	private int lastScore = 0;
	private bool synchronized = true;

	public static GameProgress LoadFromDisk() {

		GameProgress gp = new GameProgress();

		gp.bestScore = PlayerPrefs.GetInt( "BestScore", 0 );
		gp.synchronized = PlayerPrefs_GetBool( "Synchronized", true);

		return gp;
	}


	public void SaveLocal() {
		PlayerPrefs.SetInt( "BestScore", bestScore );
		PlayerPrefs_SetBool( "Synchronized", synchronized );
	}

	public bool SoundEnabled(){
		return PlayerPrefs_GetBool( "SoundEnabled", true );
	}

	public void SetSoundEnabled( bool enabled ) {
		PlayerPrefs_SetBool( "SoundEnabled", enabled );
	}


	public byte[] ToBytes() {
		return System.Text.ASCIIEncoding.Default.GetBytes(ToString());
	}

	public int BestScore {
		get {
			return bestScore;
		}
	}

	public int LastScore {
		get {
			return lastScore;
		}
	}

	public bool Synchronized {
		get {
			return synchronized;
		}
		set {
			synchronized = value;
		}
	}


	// Return true when is a new best score
	public void NewScore( int score ) {

		lastScore = score;

		if( score > bestScore ) {
			bestScore = score;
		}

		synchronized = false;
	}

	public bool IsBestScore() {
		return lastScore == bestScore;
	}


	// Overides ao playerPrefs para conseguirmos guardar booleans
	private static void PlayerPrefs_SetBool ( string name, bool value) {
		PlayerPrefs.SetInt( name, value ? 1 : 0 );
	}
	
	private static bool PlayerPrefs_GetBool ( string name ) {
		return PlayerPrefs.GetInt(name) == 1 ? true : false;
	}
	
	private static bool PlayerPrefs_GetBool ( string name, bool defaultValue ) {

		if ( PlayerPrefs.HasKey( name ) ) {
			return PlayerPrefs_GetBool( name );
		}

		return defaultValue;
	}

}
