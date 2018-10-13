using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenderExample : MonoBehaviour
{
	void Start ()
    {
        EventsManager.Instance.RouteEvent(this, new BaseEvent(EventsManager.EventType.DEFAULT));
	}
}
