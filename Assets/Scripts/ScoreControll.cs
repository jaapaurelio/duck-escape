using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using System;

public class ScoreControll : MonoBehaviour {
	

	bool started = false;
	bool counting = false;
	float gameTimeInSeconds = 0;
	int currentScore = 0;
	GameObject levelUpLabel;
	GameSceneControll gameSceneControll;

	public void Awake() {

		levelUpLabel = GameObject.Find( "LevelUpLabel" );
		gameSceneControll = GameObject.Find( "GameScene" ).GetComponent<GameSceneControll>();

	}

	// Use this for initialization
	public void StartGame() {
		started = true;

		levelUpLabel.SetActive( false );
	}

	// Get score value
	public int GetScore() {
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
			int gameTimeRounded = Mathf.RoundToInt( gameTimeInSeconds );

			if( currentScore != gameTimeRounded ) {
				currentScore = gameTimeRounded;
				GetComponent<Text>().text = currentScore.ToString();
				CheckLevel( currentScore );
			}
		}
	}

	void CheckLevel( int currentScore ) {

		// É um nivel up
		if( Array.IndexOf( GameConsts.LevelUps, currentScore ) != -1 ) {
			LevelUp();
		}
		
	}


	void LevelUp() {
		gameSceneControll.LevelUp();
		StartCoroutine( ShowLevelUp() );
	}


	IEnumerator ShowLevelUp () {

		levelUpLabel.SetActive( true );
		yield return new WaitForSeconds(1);
		levelUpLabel.SetActive( false );

	}
}
