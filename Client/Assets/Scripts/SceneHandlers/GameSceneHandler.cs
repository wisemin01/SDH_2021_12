using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

internal class GameSceneHandler : EventBehaviour
{
    public RectTransform _uiSafeArea;

    private Coroutine _gameRoutine;

    public override void OnEvent(GameEventType eventType, object token)
    {
        if (eventType == GameEventType.PLAYER_HP_CHANGED)
        {
            var hp = (int)token;

            // Player Dead.
            if (hp <= 0)
            {
                UIManager.Get().ShowAsPopup<UI_GameOver>(_uiSafeArea).Initialize(GameManager.Get().Score);
                StopGame();
            }
        }
        else if (eventType == GameEventType.GAME_RESTART_REQUEST)
        {
            StartGame();
        }
        else if (eventType == GameEventType.GAME_LEVEL_CHANGED)
        {
            UIManager.Get().ShowAsPopup<UI_LevelChanged>(_uiSafeArea).Initialize(GameManager.Get().Level);
        }
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        StartGameRoutine();
        EventManager.Get().Dispatch(GameEventType.GAME_START);
    }

    public void StopGame()
    {
        StopGameRoutine();
        EventManager.Get().Dispatch(GameEventType.GAME_STOP);
    }

    private void StartGameRoutine()
    {
        GameManager.Get().Score = 0;
        GameManager.Get().Level = 1;
        GameManager.Get().Hp = 3;

        StopGameRoutine();
        _gameRoutine = StartCoroutine(AddScoreRoutine());
    }

    private void StopGameRoutine()
    {
        if (_gameRoutine != null)
        {
            StopCoroutine(_gameRoutine);
            _gameRoutine = null;
        }
    }

    private IEnumerator AddScoreRoutine()
    {
        var yielder = new WaitForSeconds(0.1f);
        var frame = 0;

        while (true)
        {
            yield return yielder;

            GameManager.Get().Score += GameManager.Get().Level;
            EventManager.Get().Dispatch(GameEventType.GAME_SCORE_CHANGED, GameManager.Get().Score);

            frame++;
            // 게임 레벨은 20초마다 증가
            if (frame >= 200)
            {
                frame = 0;
                GameManager.Get().Level++;
            }
        }
    }
}
