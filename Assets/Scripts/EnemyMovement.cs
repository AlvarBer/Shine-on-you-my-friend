using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Movement {
	Patrol,
	Loop,
	Chasing,
}
public class EnemyMovement : CharacterMovement {

	public Transform[] waypoints;
	public Movement movementType;
	public float speedWhileChasing = 8f;

	// Extra unnecessary mutable state to hack in chasing the girl.
	public Transform currentWaypoint;
	private int waypointer = 0;
	private int waypointerDelta = 1;

	void waypointReached() {
		switch(this.movementType) {
			case Movement.Loop:
				waypointer = (waypointer + 1) % waypoints.Length;
				break;
			case Movement.Patrol:
				waypointer += waypointerDelta;
				if (waypointer == waypoints.Length) {
					waypointer--;
					waypointerDelta = -1;
				} else if (waypointer == 0) {
					waypointer = 1;
					waypointerDelta = 1;
				}
				break;
			case Movement.Chasing:
				print("BL: Character dead!");
				break;
		}
		currentWaypoint = calculateCurrentWaypoint();
	}

	void moveToWaypoint() {
		transform.position = Vector2.MoveTowards(transform.position,
											     currentWaypoint.position,
												 speed * Time.deltaTime);
	}

	void startChasing(Transform target) {
		currentWaypoint = target;
		movementType = Movement.Chasing;
		speed = speedWhileChasing;
	}

	// Probably only for debug
	Transform calculateCurrentWaypoint() {
		switch(movementType) {
			case Movement.Chasing:
				return currentWaypoint;
			default:
				return waypoints[waypointer];
		}
	}

	void Start() {
		currentWaypoint = calculateCurrentWaypoint();

		// Remove condition for non-debug
		if (movementType != Movement.Chasing) {
			transform.position = currentWaypoint.position;
		}
		// For debug
		if (movementType == Movement.Chasing) {
			this.speed = speedWhileChasing;
		}
	}

	void FixedUpdate () {
		moveToWaypoint();
		if (transform.position == currentWaypoint.position) {
			waypointReached();
		}
	}
}
