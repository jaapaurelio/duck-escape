using System;
using UnityEngine;

public class GameProgress {
	private const string PlayerPrefsKey = "duckescape-game-progress";

	// do we have modifications to write to disk/cloud?
	private bool mDirty = false;

	private int bestScore = 0;


	public static GameProgress LoadFromDisk() {
		string s = PlayerPrefs.GetString(PlayerPrefsKey, "");
		if (s == null || s.Trim().Length == 0) {
			return new GameProgress();
		}
		return GameProgress.FromString(s);
	}

	
	public static GameProgress FromBytes(byte[] b) {
		return GameProgress.FromString(System.Text.ASCIIEncoding.Default.GetString(b));
	}


	public static GameProgress FromString(string s) {
		GameProgress gp = new GameProgress();
		string[] p = s.Split(new char[] { ':' });
		if (!p[0].Equals("GPv2")) {
			Debug.LogError("Failed to parse game progress from: " + s);
			return gp;
		}

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
}
