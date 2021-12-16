using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Get()
    {
        if (_instance == null)
        {
            var go = new GameObject("[GameManager]");
            DontDestroyOnLoad(go);
            _instance = go.AddComponent<GameManager>();
        }

        return _instance;
    }

    private int _score;
    private int _level;
    private int _hp;

    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            EventManager.Get().Dispatch(GameEventType.GAME_SCORE_CHANGED, _score);
        }
    }

    public int Level
    {
        get => _level;
        set
        {
            _level = value;
            EventManager.Get().Dispatch(GameEventType.GAME_LEVEL_CHANGED, _level);
        }
    }
    public int Hp
    {
        get { return _hp; }
        set
        {
            _hp = value;
            EventManager.Get().Dispatch(GameEventType.PLAYER_HP_CHANGED, this._hp);
        }
    }
}