using UnityEngine;
using System.Collections;
using System;

public class SceneFadeInOut : MonoBehaviour
{
	public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.

	private bool sceneStarting = false;      // Whether or not the scene is still fading in.
	private bool sceneEnding = false;
	private Action callback;


	void Awake () {
		// Set the texture so that it is the the size of the screen and covers it.
		GetComponent<GUITexture>().pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
		GetComponent<GUITexture>().enabled = false;
		GetComponent<GUITexture>().color = Color.clear;

	}
	
	
	void Update () {
		// If the scene is starting...
		if( sceneStarting ) {
			StartScene();
		} else if ( sceneEnding ) {
			EndScene();
		} 
	}
	
	
	void FadeToClear () {
		// Lerp the colour of the texture between itself and transparent.
		GetComponent<GUITexture>().color = Color.Lerp(GetComponent<GUITexture>().color, Color.clear, fadeSpeed * Time.deltaTime);
	}
	
	
	void FadeToBlack () {
		// Lerp the colour of the texture between itself and black.
		GetComponent<GUITexture>().color = Color.Lerp(GetComponent<GUITexture>().color, Color.black, fadeSpeed * Time.deltaTime);
	}
	
	
	public void StartScene () {
		// Fade the texture to clear.
		FadeToClear();

		// If the texture is almost clear...
		if( GetComponent<GUITexture>().color.a <= 0.05f ) {

			// ... set the colour to clear and disable the GUITexture.
			GetComponent<GUITexture>().color = Color.clear;
			GetComponent<GUITexture>().enabled = false;
			
			// The scene is no longer starting.
			sceneStarting = false;


		}
	}


	public void ShowFader( Action cb ) {
		sceneEnding = true;
		callback = cb;
	}


	public void EndScene () {
		// Make sure the texture is enabled.
		GetComponent<GUITexture>().enabled = true;
		
		// Start fading towards black.
		FadeToBlack();
		
		// If the screen is almost black...
		if( GetComponent<GUITexture>().color.a >= 0.95f ) {
			sceneEnding = false;
			sceneStarting = true;
			callback();
		}
	}

}