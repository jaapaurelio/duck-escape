using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveControll : MonoBehaviour {

	public static SaveControll control;

	public float highScore;
	public float totalScore;
	public List<float> scoreList = new List<float>();

	void Awake(){
		if (control == null) {
			DontDestroyOnLoad(gameObject);
			control=this;
		}
		else if(control != this){
			Destroy(gameObject);
		}
	}

	public void Save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create( Application.persistentDataPath + "/gameInfo.dat" );
			
		GameData data = new GameData ();
		data.highScore = highScore;
		data.totalScore = totalScore;
		data.scoreList = scoreList;

		bf.Serialize( file, data );
		file.Close();
	}

	public void Load(){
		if(File.Exists( Application.persistentDataPath + "/gameInfo.dat" )){
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open( Application.persistentDataPath + "/gameInfo.dat", FileMode.Open );

			GameData data = (GameData) bf.Deserialize(file);
			file.Close();

			highScore = data.highScore;
			totalScore = data.totalScore;
			scoreList = data.scoreList;

		}
	}

}

[Serializable]
class GameData{
	public float highScore;
	public float totalScore;
	public List<float> scoreList = new List<float>();
}
