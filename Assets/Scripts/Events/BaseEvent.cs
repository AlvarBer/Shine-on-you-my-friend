using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEvent
{
    public EventsManager.EventType type;

    public BaseEvent(EventsManager.EventType type)
    {
        this.type = type;
    }
}