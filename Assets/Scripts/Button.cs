using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	public AudioClip UiBeepFx;

	void OnMouseUp() {

		transform.localScale += new Vector3(0.1F, 0.1F, 0);
		Beep();
		OnButtonClick();

	}

	void OnMouseDown() {
		transform.localScale -= new Vector3(0.1F, 0.1F, 0);
	}


	void Beep() {
		AudioSource.PlayClipAtPoint( UiBeepFx, Vector3.zero );
	}

	public virtual void OnButtonClick(){}

}
