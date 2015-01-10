using UnityEngine;
using System.Collections;

public class SoundToggle : Button {

	public GameObject soundMute;

	public void Start() {

		LoadPreviousSoundState();

	}

	private void LoadPreviousSoundState() {

		if ( !GameManager.Instance.Progress.SoundEnabled() ) {
			AudioListener.volume = 0;
			soundMute.renderer.enabled = true;
		} else {
			AudioListener.volume = 1;
			soundMute.renderer.enabled = false;
		}

	}

	public override void OnButtonClick() {

		if ( GameManager.Instance.Progress.SoundEnabled() ) {
			AudioListener.volume = 0;
			GameManager.Instance.Progress.SetSoundEnabled( false );
			soundMute.renderer.enabled = true;
		} else {
			AudioListener.volume = 1;
			GameManager.Instance.Progress.SetSoundEnabled( true );
			soundMute.renderer.enabled = false;
		}
	}
	
}
