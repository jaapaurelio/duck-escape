using UnityEngine;
using System.Collections;

public class SoundToggle : Button {

	public GameObject soundMute;
	
	public override void OnButtonClick() {

		if ( GameManager.Instance.IsSoundActive ) {
			AudioListener.volume = 0;
			GameManager.Instance.IsSoundActive = false;
			soundMute.renderer.enabled = true;
		} else {
			AudioListener.volume = 1;
			GameManager.Instance.IsSoundActive = true;
			soundMute.renderer.enabled = false;
		}
	}
	
}
