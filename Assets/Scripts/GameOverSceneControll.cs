using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverSceneControll : MonoBehaviour {

	ScoreControll scoreControll;
	public Transform FireworkPrefab;

	private Transform[] Fireworks = new Transform[ 4 ];

	void Start() {
		scoreControll = GameObject.Find( "Score" ).GetComponent<ScoreControll>();
	}

	public void ShowGameOver() {

		gameObject.SetActive( true );
		transform.position = new Vector3(0, 0, -10);

		GameObject.Find( "FinalScore" ).GetComponent<Text>().text = scoreControll.GetScore().ToString();
		GameObject.Find( "HighScore" ).GetComponent<Text>().text = GameManager.Instance.Progress.BestScore.ToString();

		if( GameManager.Instance.IsBestScore ) {
			StartCoroutine( ShowFireworks() );
		}

	}

	public IEnumerator ShowFireworks() {

		// First time inicialize fireworks
		if( Fireworks[0] == null ) {
			Fireworks[0] = Instantiate( FireworkPrefab, new Vector3( 5F, 0.35f, 0), Quaternion.identity) as Transform;
			Fireworks[1] = Instantiate( FireworkPrefab, new Vector3( 5F, -0.2f, 0), Quaternion.identity) as Transform;
		}

		Fireworks[0].gameObject.SetActive( false );
		Fireworks[1].gameObject.SetActive( false );

		yield return new WaitForSeconds( 0.5f );

		Fireworks[0].position = new Vector3( 1.2F, 0.35f, 0);
		Fireworks[0].gameObject.SetActive(true);

		yield return new WaitForSeconds( 0.4f );

		Fireworks[1].position = new Vector3( -1.2F, -0.2f, 0);
		Fireworks[1].gameObject.SetActive(true);

		
	}

}
