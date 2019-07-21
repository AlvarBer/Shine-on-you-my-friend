using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton("Submit")) {
			EventsManager.Instance.RouteEvent(this, new NextLevelEvent(EventsManager.EventType.NEXT_LEVEL, "Level 1"));
		}
	}
}
