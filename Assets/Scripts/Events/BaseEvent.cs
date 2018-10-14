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

public class EventWithGurl : BaseEvent
{
    public EventsManager.EventType type;
    public Transform gurl;

    public EventWithGurl(EventsManager.EventType type, Transform daGurl) : base(type) {
        this.gurl = daGurl;
    }
}