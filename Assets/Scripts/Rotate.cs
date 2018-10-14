using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Rotate : MonoBehaviour {
	[SerializeField]
	private Vector3 rotation = Vector3.forward * 10f;

	private void Update() {
		this.transform.Rotate(rotation * Time.deltaTime);
	}
}
