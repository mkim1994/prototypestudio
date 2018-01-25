using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class EventE
{
    public delegate void Handler(EventE e);
}
/*
public class ButtonPressed : EventE
{
    public string button;
    public int playerNum;
    public ButtonPressed(string _button)
    {
        button = _button;
    }

public class Reset : EventE { }
}*/

public class HandTouchedEvent : EventE { }

public class HandRejectedEvent : EventE {}

public class EventManager
{

    // Just some singleton boiler plate
    static private EventManager instance;
    static public EventManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EventManager();
            }
            return instance;
        }
    }

    // Here is the core of the system: a dictionary that maps types (specifically Event types in this case)
    // to Event.Handlers
    private Dictionary<Type, EventE.Handler> registeredHandlers = new Dictionary<Type, EventE.Handler>();

    // This is where you can add handlers for events. We use generics for 2 reasons:
    // 1. Passing around Type objects can be tedious and verbose
    // 2. Using generics allows us to add a little type safety, by getting
    // the compiler to ensure we're registering for an Event type and not some other random type
    public void Register<T>(EventE.Handler handler) where T : EventE
    {
        // NOTE: This method does not check to see if a handler was registered twice
        // I would add an assert here, but you can add in a more permanent check too if interested
        Type type = typeof(T);
        if (registeredHandlers.ContainsKey(type))
        {
            registeredHandlers[type] += handler;
        }
        else
        {
            registeredHandlers[type] = handler;
        }
    }

    // This is where you stop listening to an event. Make sure to balance
    // any calls to Register with corresponding calls to Unregister
    public void Unregister<T>(EventE.Handler handler) where T : EventE
    {
        Type type = typeof(T);
        EventE.Handler handlers;
        if (registeredHandlers.TryGetValue(type, out handlers))
        {
            handlers -= handler;
            if (handlers == null)
            {
                registeredHandlers.Remove(type);
            }
            else
            {
                registeredHandlers[type] = handlers;
            }
        }
    }

    // This is how you "publish" and event. All it entails is looking up
    // the event type and calling the delegate containing all the handlers
    public void Fire(EventE e)
    {
        Type type = e.GetType();
        EventE.Handler handlers;
        if (registeredHandlers.TryGetValue(type, out handlers))
        {
            handlers(e);
        }
    }
}


/*
public abstract class EventE {
    public delegate void Handler(EventE e);
}

// EVENTS
public class ButtonPressed : EventE {
    public string button;
    public int playerNum;
    public ButtonPressed(string _button)
    {
        button = _button;
    }
}

public class HandTouchedEvent : EventE{
}

public class Reset : EventE { }

public class EventManager {

	public delegate void EventDelegate<T>(T e) where T: EventE;
	private delegate void EventDelegate(EventE e);

	private Dictionary <System.Type, EventDelegate> delegates = new Dictionary<System.Type, EventDelegate>();
	private Dictionary<System.Delegate, EventDelegate> delegateLookup = new Dictionary<System.Delegate, EventDelegate>();
	private List<EventE> queuedEvents = new List<EventE> ();
	private object queueLock = new object();

	public void Register<T> (EventDelegate<T> del) where T: EventE {
		if (delegateLookup.ContainsKey (del)) {
			return;
		}

		EventDelegate internalDelegate = (e) => del ((T)e);
		delegateLookup [del] = internalDelegate;

		EventDelegate tempDel;
		if (delegates.TryGetValue (typeof(T), out tempDel)) {
			delegates [typeof(T)] = tempDel + internalDelegate;
		} else {
			delegates [typeof(T)] = internalDelegate;
		}
	}

	public void Unregister<T> (EventDelegate<T> del) where T: EventE {
		EventDelegate internalDelegate;
		if (delegateLookup.TryGetValue (del, out internalDelegate)) {
			EventDelegate tempDel;
			if (delegates.TryGetValue (typeof(T), out tempDel)) {
				tempDel -= internalDelegate;
				if (tempDel == null) {
					delegates.Remove (typeof(T));
				} else {
					delegates [typeof(T)] = tempDel;
				}
			}
			delegateLookup.Remove (del);
		}
	}

	public void Clear(){
		lock (queueLock) {
			if (delegates != null) {
				delegates.Clear ();
			}
			if (delegateLookup != null) {
				delegateLookup.Clear ();
			}
			if (queuedEvents != null) {
				queuedEvents.Clear ();
			}
		}
	}

	public void Fire(EventE e){
		EventDelegate del;
		if (delegates.TryGetValue (e.GetType (), out del)) {
			del.Invoke (e);
		}
	}

	public void ProcessQueuedEvents(){
		List<EventE> events;
		lock (queueLock) {
			if (queuedEvents.Count > 0) {
				events = new List<EventE> (queuedEvents);
				queuedEvents.Clear ();
			} else {
				return;
			}
		}

		foreach (EventE e in events) {
			Fire (e);
		}
	}

	public void Queue(EventE e){
		lock (queueLock) {
			queuedEvents.Add (e);
		}
	}

}
*/