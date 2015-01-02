using UnityEngine;
using System.Collections;

public class SoundToggle : Button {


	private AudioSource[] allAudioSources;
	
	public override void OnButtonClick() {
		allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];

		foreach( AudioSource audioS in allAudioSources) {
			audioS.mute = true;
		}
	}
	
}
