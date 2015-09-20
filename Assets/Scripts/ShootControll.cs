using UnityEngine;
using System.Collections;

public class ShootControll : MonoBehaviour {

	void Awake () {
		// Set the texture so that it is the the size of the screen and covers it.
		GetComponent<GUITexture>().pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
		GetComponent<GUITexture>().enabled = false;
		
	}

	public void Shoot() {
		StartCoroutine(ShowShoot());
	}

	// Mostra o ecrã branco 
	public IEnumerator ShowShoot() {

		GetComponent<GUITexture>().enabled = true;

		yield return new WaitForSeconds(0.1f);

		GetComponent<GUITexture>().enabled = false;

	}

}
