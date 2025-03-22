using System.Collections.Generic;
using UnityEngine.Events;

public class EventManager
{
    static readonly Dictionary<GameEvent, UnityEvent<object>> _events = new();

    public static void StartListening(GameEvent eventName, UnityAction<object> callback)
    {
        if (_events.TryGetValue(eventName, out UnityEvent<object> thisEvent))
        {
            thisEvent.AddListener(callback);
        }
        else
        {
            thisEvent = new UnityEvent<object>();
            thisEvent.AddListener(callback);

            _events.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(GameEvent eventName, UnityAction<object> callback)
    {
        if (_events.TryGetValue(eventName, out UnityEvent<object> thisEvent))
        {
            thisEvent.RemoveListener(callback);
        }
    }

    public static void EmitEvent(GameEvent eventName, object data = null)
    {
        if (_events.TryGetValue(eventName, out UnityEvent<object> thisEvent))
        {
            thisEvent.Invoke(data);
        }
    }
}

 public enum GameEvent
{
    Despawn,
}