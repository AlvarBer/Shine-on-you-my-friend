using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CharacterMovement {

	public Vector2 direction;
	protected void Move() {
		var posDelta = (direction * speed);
		transform.SetPositionAndRotation(
			transform.position + new Vector3(posDelta.x, posDelta.y, 0),
			transform.rotation
		);
	}
	void FixedUpdate () {
		this.direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		Move();
	}

	private void Awake()
    {
        EventsManager.Instance.SubscribeTo(EventsManager.EventType.TARGET_DEATH, OnPlayerDeath);
    }

    private void OnPlayerDeath(object sender, BaseEvent e)
    {
		print("BL: YOU DIED");
    }
}
