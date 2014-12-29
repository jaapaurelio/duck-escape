using System;
using UnityEngine;

public class GameProgress {
	private const string PlayerPrefsKey = "duckescape-game-progress";

	// do we have modifications to write to disk/cloud?
	private bool mDirty = false;

	private int bestScore = 0;


	public static GameProgress LoadFromDisk() {
		PlayerPrefs.SetString(PlayerPrefsKey, "0" );
		string s = PlayerPrefs.GetString(PlayerPrefsKey, "");

		if (s == null || s.Trim().Length == 0) {
			return new GameProgress();
		}

		return GameProgress.FromString(s);
	}


	public void SaveToDisk() {
		PlayerPrefs.SetString(PlayerPrefsKey, ConvertToString() );
		mDirty = false;
	}

	private String ConvertToString() {
		return "" + bestScore;
	}
	
	public static GameProgress FromBytes(byte[] b) {
		return GameProgress.FromString(System.Text.ASCIIEncoding.Default.GetString(b));
	}


	public static GameProgress FromString( string s ) {
		GameProgress gp = new GameProgress();

		gp.bestScore = System.Convert.ToInt32( s );

		return gp;
	}

	public void MergeWith(GameProgress other) {

	}

	public byte[] ToBytes() {
		return System.Text.ASCIIEncoding.Default.GetBytes(ToString());
	}

	public bool Dirty {
		get {
			return mDirty;
		}
		set {
			mDirty = value;
		}
	}

	public int BestScore {
		get {
			return bestScore;
		}
	}

	// Return true when is a new best score
	public bool SetScore( int score ) {

		if( score > bestScore ) {
			bestScore = score;
			mDirty = true;

			return true;
		}

		return false;

	}
}
