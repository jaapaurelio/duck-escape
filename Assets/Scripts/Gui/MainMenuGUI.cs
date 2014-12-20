using UnityEngine;
using System.Collections;

public class MainMenuGUI : MonoBehaviour {

	private static bool sAutoAuthenticate = true;

	void Start() {

		// if this is the first time we're running, bring up the sign in flow
		if (sAutoAuthenticate) {
			GameManager.Instance.Authenticate();
			sAutoAuthenticate = false;
		}
	}

}
