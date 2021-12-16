using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public enum GameEventType
{
    GAME_START,
    GAME_STOP,

    GAME_SCORE_CHANGED,
    GAME_LEVEL_CHANGED,

    GAME_RESTART_REQUEST,

    PLAYER_HP_CHANGED,
    PLAYER_ATTACKED,
}

public interface IEventListener
{
    void OnEvent(GameEventType eventType, object token);
}

public class EventManager : MonoBehaviour
{
    private static EventManager _instance;

    private readonly List<IEventListener> _eventListeners = new List<IEventListener>();

    public static bool HasInstance
    {
        get => _instance != null;
    }

    public static EventManager Get()
    {
        if (_instance == null)
        {
            var go = new GameObject("[EventManager]");
            DontDestroyOnLoad(go);
            _instance = go.AddComponent<EventManager>();
        }

        return _instance;
    }

    public void Register(IEventListener listener)
    {
        if (listener == null || _eventListeners.Contains(listener))
        {
            return;
        }

        _eventListeners.Add(listener);
    }

    public void Unregister(IEventListener listener)
    {
        if (listener == null)
        {
            return;
        }

        _eventListeners.Remove(listener);
    }

    public void Dispatch(GameEventType type)
    {
        foreach (var listener in _eventListeners)
        {
            listener?.OnEvent(type, null);
        }
    }

    public void Dispatch(GameEventType type, object token)
    {
        foreach (var listener in _eventListeners)
        {
            listener?.OnEvent(type, token);
        }
    }
    private void OnDestroy()
    {
        _eventListeners.Clear();
    }
}
