using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour {

	private bool isOn;

	public void Off() {
		isOn = false;
	}

	public void On() {
		isOn = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (isOn) {
			GetComponent<SpriteRenderer>().color = Color.green;
		} else {
			GetComponent<SpriteRenderer>().color = Color.white;
		}	
	}
}
