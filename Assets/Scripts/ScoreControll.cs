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
	float score = 0;
	GameObject highScore;
	float timeToLevelUp = LEVEL_TIME;
	GameObject levelUpLabel;
	GameObject aim;
	AimControll aimControll;

	public void Start() {
		levelUpLabel = GameObject.Find( "LevelUpLabel" );
		aim = GameObject.Find( "Aim" );

		aimControll = aim.GetComponent<AimControll>();
	}

	// Use this for initialization
	public void StartGame() {
		highScore = GameObject.Find( "HighScore" );
		highScore.GetComponent<Text>().text = SaveControll.control.highScore.ToString();
		started = true;

		levelUpLabel.SetActive( false );
		timeToLevelUp = LEVEL_TIME;
	}

	// Get score value
	public float Get() {
		return float.Parse( GetComponent<Text>().text );
	}

	// Hide score
	public void Hide() {
		GetComponent<Text>().enabled = false;
	}

	// Reset score
	public void Reset() {
		score = 0;
		GetComponent<Text>().text = score.ToString();
	}

	// Show score
	public void Show(){
		GetComponent<Text>().enabled = true;		
	}

	// Stop score count
	public void Stop() {
		score = Get();
		setHighScore ();
		addScore();
		getScores();
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

	void setHighScore(){

		long scoreL = (long) score;

		if( score > SaveControll.control.highScore ) {
			SaveControll.control.highScore = score;

			highScore.GetComponent<Text>().text = SaveControll.control.highScore.ToString();

			// Set highscore to the leaderboard of google play services
			Social.ReportScore( scoreL, "CgkIrPzUj8oPEAIQBg", (bool success) => {

				// handle success or failure
				if( success ){
					Debug.Log( "adicionado com sucesso" );
				}else{
					Debug.Log( "falhou" );
				}

			});
		}
	}
	
	// Add scores
	void addScore(){
		
		// set total score
		SaveControll.control.totalScore += score;
		// add scores to list
		//SaveControll.control.scoreList.Add (score);

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
			score += Time.deltaTime;
			
			float seconds = Mathf.RoundToInt( score % 60 );
			
			GetComponent<Text>().text = seconds.ToString();

			updateLevelTimer();
		}



	}

	void updateLevelTimer() {
		
		timeToLevelUp -= Time.deltaTime;
		
		if( timeToLevelUp < 0) {
			timeToLevelUp = LEVEL_TIME;
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
