using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class RankingButton : MonoBehaviour {

	void OnMouseDown () {

		// authenticate user:
		Social.localUser.Authenticate((bool success) => {
			// handle success or failure
			if( success ){
				/// show leaderboard UI
				Social.ShowLeaderboardUI();			
			}else{
				//Debug.Log("login insucesso");
			}
			
		});

	}
}
