using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventSystemE
{

    // Regular synchronous implementation goes here ...

    // Keep a simple list of the events you want to queue
    // for later processing
    private List<EventE> queuedEvents = new List<EventE>();

    // Call this method to queue an event...
    public void QueueEvent(EventE e)
    {
        // insert at the head of the line since the
        // queue will be processed in reverse order
        queuedEvents.Insert(0, e);

        // NOTE: To my knowledge this method makes
        // NO guarantees regarding thread safety and you
        // should not use this with multiple threads
    }

    // Call this method when you want to process all the
    // pending events
    public void ProcessQueuedEvents()
    {
        // NOTE: processing the queue in reverse order to
        // avoid a concurrent modification exception if
        // an event causes another event to be queued
        // while processing the events
        for (int i = queuedEvents.Count - 1; i >= 0; --i)
        {
            //Fire(queuedEvents[i]);
            Services.EventManager.Fire(queuedEvents[i]);
            queuedEvents.RemoveAt(i);
        }
    }
}
