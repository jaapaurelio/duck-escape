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
		guiTexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
		guiTexture.enabled = false;
		guiTexture.color = Color.clear;

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
		guiTexture.color = Color.Lerp(guiTexture.color, Color.clear, fadeSpeed * Time.deltaTime);
	}
	
	
	void FadeToBlack () {
		// Lerp the colour of the texture between itself and black.
		guiTexture.color = Color.Lerp(guiTexture.color, Color.black, fadeSpeed * Time.deltaTime);
	}
	
	
	public void StartScene () {
		// Fade the texture to clear.
		FadeToClear();

		// If the texture is almost clear...
		if( guiTexture.color.a <= 0.05f ) {

			// ... set the colour to clear and disable the GUITexture.
			guiTexture.color = Color.clear;
			guiTexture.enabled = false;
			
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
		guiTexture.enabled = true;
		
		// Start fading towards black.
		FadeToBlack();
		
		// If the screen is almost black...
		if( guiTexture.color.a >= 0.95f ) {
			sceneEnding = false;
			sceneStarting = true;
			callback();
		}
	}

}