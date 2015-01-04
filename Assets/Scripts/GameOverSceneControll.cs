using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverSceneControll : MonoBehaviour {

	public GoogleAnalyticsV3 googleAnalytics;
	
	public ParticleSystem FireworkEffect;
	public AudioClip FireworkSound;

	private GameObject newScoreNotification;

	private float timeToNewFirework = 0;
	
	void Start() {
		newScoreNotification = GameObject.Find( "NewScoreNotification" );
	}

	void Update() {

		if( GameManager.Instance.Progress.IsBestScore() ) {
			ShowFireworks();
		}
	}

	public void ShowGameOver() {


		gameObject.SetActive( true );
		transform.position = new Vector3(0, 0, -10);

		if( GameManager.Instance.Progress.IsBestScore() ) {
			newScoreNotification.SetActive( true );

			googleAnalytics.LogEvent(new EventHitBuilder()
			                         .SetEventCategory("Score")
			                         .SetEventAction("Best Score")
			                         .SetEventValue( GameManager.Instance.Progress.LastScore ));
		} else {
			newScoreNotification.SetActive( false );

			googleAnalytics.LogEvent(new EventHitBuilder()
			                         .SetEventCategory("Score")
			                         .SetEventAction("Lower Score")
			                         .SetEventValue( GameManager.Instance.Progress.LastScore ));

		}

		GameObject.Find( "FinalScore" ).GetComponent<Text>().text = GameManager.Instance.Progress.LastScore.ToString();
		GameObject.Find( "HighScore" ).GetComponent<Text>().text = GameManager.Instance.Progress.BestScore.ToString();

	}

	public void ShowFireworks() {

		timeToNewFirework -= Time.deltaTime;

		if( timeToNewFirework < 0 ) {

			float xPosition = Random.Range( -1.6f, 1.6f );
			float yPosition = Random.Range( -0.23f, 0.6f );
			Vector3 fireworkPosition = new Vector3( xPosition, yPosition, 0 );

			AudioSource.PlayClipAtPoint( FireworkSound, fireworkPosition );
			var p1 = Instantiate( FireworkEffect, fireworkPosition, Quaternion.identity) as ParticleSystem;
			p1.transform.parent = transform;
			Destroy( p1.gameObject, p1.startLifetime );

			timeToNewFirework = Random.Range( 0.4f, 1f );

		}

	}

}
