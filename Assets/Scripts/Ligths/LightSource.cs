using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour {
	// TODO: 4 directions
	public enum Rotation {Horizontal, Vertical};
	private bool isOn;
	[SerializeField]
	private Rotation rotation;
	[SerializeField]
	private float shiftSpeed = 25f;
	private Emitter emitter;
	private SpriteRenderer spriteRenderer;

	private void Awake() {
		emitter = GetComponent<Emitter>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public void Off() {
		isOn = false;
		emitter.gameObject.SetActive(false);
	}

	public void On() {
		isOn = true;
		emitter.gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	private void Update () {
		if (isOn) {
			ReadInput();
			spriteRenderer.color = Color.green;
		} else {
			spriteRenderer.color = Color.white;
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
		emitter.angle += inputDelta * shiftSpeed * Time.deltaTime;
	}
}
