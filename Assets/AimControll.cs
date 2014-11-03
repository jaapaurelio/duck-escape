using UnityEngine;
using System.Collections;

public class AimControll : MonoBehaviour {

	// Use this for initialization
	public void StartGame () {

		rigidbody2D.AddForce( new Vector2( 25, 25 ) );
	}

}
