using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class ScoreControll : MonoBehaviour {

	static float LEVEL_TIME = 5.0f;	// secconds

	bool started = false;
	bool counting = false;
	float gameTimeInSeconds = 0;
	float currentScore = 0;
	GameObject levelUpLabel;
	GameObject aim;
	AimControll aimControll;
	string leaderBoardId = "CgkIrPzUj8oPEAIQBg";

	public void Start() {
		levelUpLabel = GameObject.Find( "LevelUpLabel" );
		aim = GameObject.Find( "Aim" );

		aimControll = aim.GetComponent<AimControll>();
	}

	// Use this for initialization
	public void StartGame() {
		started = true;

		levelUpLabel.SetActive( false );
	}

	// Get score value
	public float GetScore() {
		return currentScore;
	}

	// Hide score
	public void Hide() {
		GetComponent<Text>().enabled = false;
	}

	// Reset score
	public void Reset() {
		gameTimeInSeconds = 0;
		currentScore = 0;
		GetComponent<Text>().text = currentScore.ToString();
	}

	// Show score
	public void Show(){
		GetComponent<Text>().enabled = true;		
	}

	// Stop score count
	public void Stop() {
		//SetHighScore();
		//addScore();
		started = false;
		counting = false;
	}
	
	// Get highscore
	public float getBestScore() {
		return SaveControll.control.highScore;
	}
	
	// Get total scores
	public float getTotalScores() {
		return SaveControll.control.totalScore;
	}

	public void SetHighScore(){
		long scoreL = (long) currentScore;

		if( currentScore > SaveControll.control.highScore ) {
			SaveControll.control.highScore = currentScore;

			// Set highscore to the leaderboard of google play services
			Social.ReportScore( scoreL, leaderBoardId, (bool success) => {
				// adicionado
			});
		}
	}
	
	// Add scores
	public void addScore(){
		
		// set total score
		SaveControll.control.totalScore += currentScore;
		// add scores to list
		//SaveControll.control.scoreList.Add (currentScore);

		//totalScore.GetComponent<Text>().text = getTotalScores().ToString();
		
	}
	
	// Add scores
	void getScores(){
		
		//foreach (float value in SaveControll.control.scoreList) {
		//	Debug.Log( "value " + value );
		//}
		
	}
	
	// Update is called once per frame
	void Update() {
	
		if( !started ) {
			return;
		}

		// No primeiro click após iniciar activa o contador
		if( !counting ) {

			if ( Input.GetMouseButtonDown (0) ) {
				counting = true;
			}
		}


		if( counting ) {
			gameTimeInSeconds += Time.deltaTime;
			
			float gameTimeRounded = Mathf.RoundToInt( gameTimeInSeconds % 60 );

			if( currentScore != gameTimeRounded ) {
				currentScore = gameTimeRounded;
				GetComponent<Text>().text = currentScore.ToString();
				CheckLevel( currentScore );
			}
		}



	}

	void CheckLevel( float currentScore ) {
		
		if( currentScore % LEVEL_TIME == 0 ) {
			LevelUp();
		}
		
	}

	void LevelUp() {
		aimControll.LevelUp();
		StartCoroutine( ShowLevelUp() );
	}

	IEnumerator ShowLevelUp () {

		levelUpLabel.SetActive( true );
		yield return new WaitForSeconds(1);
		levelUpLabel.SetActive( false );

	}
}
