using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour {
	public enum Rotation {Horizontal, Vertical};
	private bool isOn;
	[SerializeField]
	private Rotation rotation;
	[SerializeField]
	private float shiftSpeed = 25f;
	[SerializeField]
	private Emitter emitter;

	public void Off() {
		isOn = false;
	}

	public void On() {
		isOn = true;
	}
	
	// Update is called once per frame
	private void Update () {
		ReadInput();
		if (isOn) {
			GetComponent<SpriteRenderer>().color = Color.green;
		} else {
			GetComponent<SpriteRenderer>().color = Color.white;
		}
	}

	private void ReadInput() {
		float inputDelta;
		if (rotation == Rotation.Horizontal) {
			inputDelta = Input.GetAxisRaw("Horizontal");
		}
		else {
			inputDelta = Input.GetAxisRaw("Vertical");
		}
		emitter.angleChange += inputDelta * shiftSpeed * Time.deltaTime;
	}
}
