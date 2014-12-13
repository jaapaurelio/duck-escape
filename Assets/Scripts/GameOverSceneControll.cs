using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverSceneControll : MonoBehaviour {

	ScoreControll scoreControll;

	void Start() {
		scoreControll = GameObject.Find( "Score" ).GetComponent<ScoreControll>();
	}

	public void ShowGameOver() {

		gameObject.SetActive( true );
		transform.position = new Vector3(0, 0, -10);

		GameObject.Find( "HighScore" ).GetComponent<Text>().text = scoreControll.getBestScore().ToString();
		GameObject.Find( "FinalScore" ).GetComponent<Text>().text = scoreControll.GetScore().ToString();

	}

}
