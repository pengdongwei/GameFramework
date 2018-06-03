using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsMgr : ISingleton<EventsMgr>
{

    public delegate void Handler(object userdata);

    public void AttachEvent(EventsType type, Handler handler) {
    }

    public void DetachEvent(EventsType type, Handler handler)
    {
    }
}
