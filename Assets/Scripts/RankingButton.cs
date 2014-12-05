using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class RankingButton : MonoBehaviour {

	void OnMouseDown () {

		// show leaderboard UI
		Social.ShowLeaderboardUI();

	}
}
