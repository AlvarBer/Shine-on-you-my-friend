using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))] 
public class CharacterMovement : MonoBehaviour {

	public float speed = 4f;
	public Vector2 direction;

	protected void Move() {
		GetComponent<Rigidbody2D>().velocity = direction * speed;
		// print("BL: Object(" + this.name + ") has vect " + direction);
	}
}
