using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class UI_PlayerHp : EventBehaviour
{
    public ObjectPool pool;

    private List<GameObject> _hearts = new List<GameObject>();

    public void Refresh()
    {
        // 기존 Object 모두 클리어
        foreach (var heart in _hearts)
        {
            pool.Return(heart);
        }
        _hearts.Clear();

        // 플레이어 HP 에 맞춰 새로 생성.
        for (int i = 0; i < GameManager.Get().Hp; ++i)
        {
            var go = pool.Get();

            go.transform.SetParent(this.transform);
            go.transform.localScale = Vector3.one;

            _hearts.Add(go);
        }
    }

    public override void OnEvent(GameEventType eventType, object token)
    {
        if (eventType == GameEventType.PLAYER_HP_CHANGED ||
            eventType == GameEventType.GAME_START)
        {
            Refresh();
        }
    }
}
