using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {
	private void Update() {
		if (Input.GetButtonDown("Submit")) {
			EventsManager.Instance.RouteEvent(this, new NextLevelEvent(EventsManager.EventType.NEXT_LEVEL, "Menu"));
		}
	}
}
