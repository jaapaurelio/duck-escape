using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class RankingButton : Button {

	public override void OnButtonClick() {

		GameManager.Instance.ShowLeaderboardUI();

	}
}
