using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenerExample : MonoBehaviour
{
    private void Awake()
    {
        EventsManager.Instance.SubscribeTo(EventsManager.EventType.DEFAULT, OnBaseEvent);
    }

    private void OnBaseEvent(object sender, BaseEvent e)
    {
        // Handle Event
    }
}
