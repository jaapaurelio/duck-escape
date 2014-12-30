using UnityEngine;
using System.Collections;

public class SortParticleSystem : MonoBehaviour {

	string LayerName = "Particles";

	public void Start() {
		particleSystem.renderer.sortingLayerName = LayerName;
	}

	public void update() {


	}
}
