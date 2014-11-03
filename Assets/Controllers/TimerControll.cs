using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerControll : MonoBehaviour {

	bool started = false;
	float timer = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if( started ) {

			timer += Time.deltaTime;

			float seconds = Mathf.RoundToInt( timer % 60 );
	
			GetComponent<Text>().text = seconds.ToString();

		}
	}

	public void StartGame() {

		started = true;
	}
}
