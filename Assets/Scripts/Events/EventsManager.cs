using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : Singleton<EventsManager>
{
    public enum EventType
    {
        TARGET_OVERLAP,
        DEFAULT,
        TARGET_DEATH,
        NEXT_LEVEL,
    }

    public delegate void EventHandler(object sender, BaseEvent e);

    private Dictionary<EventType, List<EventHandler>> _eventHandlers;

    protected override void Awake()
    {
        base.Awake();

        _eventHandlers = new Dictionary<EventType, List<EventHandler>>();
    }

    public void SubscribeTo(EventType type, EventHandler handler)
    {
        if (!_eventHandlers.ContainsKey(type))
        {
            _eventHandlers.Add(type, new List<EventHandler>());
        }
        _eventHandlers[type].Add(handler);
    }

    public void Unsubscribe(EventType type, EventHandler handler)
    {
        List<EventHandler> handlers = _eventHandlers[type];

        if (handlers != null)
        {
            handlers.Remove(handler);
        }
    }

    public void RouteEvent(object sender, BaseEvent e)
    {
        if (_eventHandlers.ContainsKey(e.type))
        {
            List<EventHandler> handlers = _eventHandlers[e.type];
            for (int i = 0; i < handlers.Count; i++)
            {
                handlers[i](sender, e);
            }
        }
    }
}