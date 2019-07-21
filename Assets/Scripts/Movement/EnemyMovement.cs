using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Movement {
	Patrol,
	Loop,
	Chasing,
}


[RequireComponent (typeof (Collider2D))] 

public class EnemyMovement : CharacterMovement {

	public Transform[] waypoints;
	public Movement movementType;
	public float speedWhileChasing = 8f;
	public LayerMask girlLayer;
	public float startSpeed;
	private Movement startMovement;

	// Extra unnecessary mutable state to hack in chasing the girl.
	public Transform currentWaypoint;
	public int waypointer = 0;
	public int waypointerDelta = 1;

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
				// This block is intentioally left blank
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

	public void OnPlayerSeen(object sender, BaseEvent e) {
        var realEvent = (EventWithGurl)e;
		startChasing(realEvent.gurl);
    }

	private void OnPlayerCollission() {
		EventsManager.Instance.RouteEvent(this, new BaseEvent(EventsManager.EventType.TARGET_DEATH));
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "LaPollaDeLaura") {
			OnPlayerCollission();
		}
		else if (collision.collider.tag == "Light") {
			this.movementType= startMovement;
			this.waypointer = 0;
			this.speed = startSpeed;
			this.waypointReached();
		}
	}

	void Start() {
		startSpeed = this.speed;
		startMovement = this.movementType;
		EventsManager.Instance.SubscribeTo(EventsManager.EventType.TARGET_OVERLAP, OnPlayerSeen);
		currentWaypoint = calculateCurrentWaypoint();
		transform.position = currentWaypoint.position;
	}

	void FixedUpdate () {
		moveToWaypoint();
		if (transform.position == currentWaypoint.position) {
			waypointReached();
		}
	}
}
