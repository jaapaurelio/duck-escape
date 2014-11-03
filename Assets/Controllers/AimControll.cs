using UnityEngine;
using System.Collections;

public class AimControll : MonoBehaviour {
	
	public void StartGame () {

		// Aplica a força para mover a mira
		rigidbody2D.AddForce( new Vector2( 25, 25 ) );
	}

}
