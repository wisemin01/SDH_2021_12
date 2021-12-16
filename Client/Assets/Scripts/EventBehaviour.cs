using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class EventBehaviour : MonoBehaviour, IEventListener
{
    protected virtual void Awake()
    {
        if (Application.isPlaying)
        {
            EventManager.Get().Register(this);
        }
    }

    protected virtual void OnDestroy()
    {
        if (Application.isPlaying)
        {
            // Get() 에서 new GameObject 를 하는데
            // OnDestroy() 에서는 호출하면 안된다.
            if (EventManager.HasInstance)
            {
                EventManager.Get().Unregister(this);
            }
        }
    }

    public abstract void OnEvent(GameEventType eventType, object token);
}
