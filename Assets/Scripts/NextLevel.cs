using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour {

	[SerializeField]
	private string nextLevel;

	void Start () {
		EventsManager.Instance.SubscribeTo(EventsManager.EventType.PLAYER_OVERLAP_EXIT, OnPlayerOverlapMe);
	}
	public void OnPlayerOverlapMe (object o, BaseEvent e) {
		EventsManager.Instance.RouteEvent(this, new NextLevelEvent(EventsManager.EventType.NEXT_LEVEL, nextLevel));
	}
}
