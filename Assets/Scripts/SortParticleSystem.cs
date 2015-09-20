using UnityEngine;
using System.Collections;

public class SortParticleSystem : MonoBehaviour {

	string LayerName = "Particles";

	public void Awake() {
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = LayerName;
	}

	public void update() {


	}
}
