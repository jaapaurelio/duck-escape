﻿using UnityEngine;
using System.Collections;

public class GameManagerControll : MonoBehaviour {

	bool started = false;
	GameObject gameScene;
	GameObject mainMenuScene;
	GameObject aim;
	GameObject timer;
	GameObject duck;
	GameObject screenFader;
	AimControll aimControll;
	TimerControll timerControll;
	DuckControll duckControll;
	SceneFadeInOut sceneFadeInOut;
	string activeScene = "MENU";


	// Use this for initialization
	void Start () {

		aim = GameObject.Find( "Aim" );
		timer = GameObject.Find( "Timer" );
		duck = GameObject.Find( "Duck" );
		gameScene = GameObject.Find( "GameScene" );
		mainMenuScene = GameObject.Find( "MainMenuScene" );
		screenFader = GameObject.Find( "ScreenFader" );

		aimControll = aim.GetComponent<AimControll>();
		timerControll = timer.GetComponent<TimerControll>();
		duckControll = duck.GetComponent<DuckControll>();
		sceneFadeInOut = screenFader.GetComponent<SceneFadeInOut>();

		StartMainMenuScene();

	}

	void HideAllScenes() {
		gameScene.SetActive( false );
		mainMenuScene.SetActive( false );
	}

	public void StartMainMenuScene() {
		HideAllScenes();
		mainMenuScene.SetActive( true );
	}

	public void StartGameScene() {
		HideAllScenes();

		sceneFadeInOut.ShowFader( StartGame );

	}

	public void StartGame() {
		gameScene.SetActive( true );
	}
	
	// Update is called once per frame
	void Update () {

		if( activeScene == "GAME" ) {
			if( !started ) {
				// No primeiro toque começa o jogo
				if ( Input.GetMouseButtonDown (0) ) {
					duckControll.StartGame();
					aimControll.StartGame();
					timerControll.StartGame();
					started = true;
				}
			}
		}
	}
}
