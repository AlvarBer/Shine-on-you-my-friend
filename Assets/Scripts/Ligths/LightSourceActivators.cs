using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSourceActivators : MonoBehaviour {

	// Because negative int modulo sucks
	int mod(int x, int m) {
		return (x%m + m)%m;
	}

	public LightSource[] lightSources;
	private int lightSourceIndex;

	void Start () {
		foreach (var light in lightSources) {
			light.Off();
		}
		lightSources[lightSourceIndex].On();
	}
	
	void moveIndex(int delta) {
		lightSources[lightSourceIndex].Off();
		lightSourceIndex = mod((lightSourceIndex + delta), lightSources.Length);
		lightSources[lightSourceIndex].On();
	}

	void Update () {
		var f = Input.GetButtonDown("Switch Lights Forwards");
		var b = Input.GetButtonDown("Switch Lights Backwards");
		if (f && !b) {
			moveIndex(1);
		} 
		if (b && !f) {
			moveIndex(-1);
		}
	}
}
