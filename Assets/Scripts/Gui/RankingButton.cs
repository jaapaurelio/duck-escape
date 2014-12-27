using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class RankingButton : MonoBehaviour {

	void OnMouseDown () {

		GameManager.Instance.ShowLeaderboardUI();

	}
}
