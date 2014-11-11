using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveControll : MonoBehaviour {

	public static SaveControll control;

	public float highScore;

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
		}
	}

}

[Serializable]
class GameData{
	public float highScore;
}
