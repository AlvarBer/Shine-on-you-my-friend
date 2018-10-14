using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CharacterMovement {

	public Vector2 direction;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected void Move() {
		var posDelta = (direction * speed);
		transform.SetPositionAndRotation(
			transform.position + new Vector3(posDelta.x, posDelta.y, 0),
			transform.rotation
		);
	}

    protected void Animate()
    {
        if (direction.x >= 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        animator.SetFloat("DirectionX", direction.x);
        animator.SetFloat("DirectionY", direction.y);
    }

	void FixedUpdate () {
		this.direction = new Vector2(Input.GetAxisRaw("Horizontal1"), Input.GetAxisRaw("Vertical1"));
		Move();
        Animate();
	}
}
