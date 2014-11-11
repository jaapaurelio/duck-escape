using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreControll : MonoBehaviour {

	bool started = false;
	float score = 0;
	GameObject highScore;

	// Use this for initialization
	public void StartGame() {
		highScore = GameObject.Find( "HighScore" );
		highScore.GetComponent<Text>().text = SaveControll.control.highScore.ToString();
		started = true;
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
		started = false;
	}

	void setHighScore(){

		if( score > SaveControll.control.highScore ) {
			SaveControll.control.highScore = score;

			highScore.GetComponent<Text>().text = SaveControll.control.highScore.ToString();
		}
	}
	
	// Update is called once per frame
	void Update() {
	
		if( started ) {
			
			score += Time.deltaTime;
			
			float seconds = Mathf.RoundToInt( score % 60 );
			
			GetComponent<Text>().text = seconds.ToString();
			
		}

	}
}
