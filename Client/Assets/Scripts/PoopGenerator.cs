using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopGenerator : EventBehaviour
{
    public ObjectPool objectPool;

    private Coroutine _spawnPoopsRoutine;
    private List<GameObject> _spawnedPoops = new List<GameObject>();

    public void StartPoopsRoutine(int level, bool clearPoops)
    {
        if (_spawnPoopsRoutine != null)
        {
            StopCoroutine(_spawnPoopsRoutine);
            _spawnPoopsRoutine = null;
        }

        _spawnPoopsRoutine = StartCoroutine(SpawnPoops(level, clearPoops));
    }

    IEnumerator SpawnPoops(int level, bool clearPoops)
    {
        // 소환된 똥이 있다면 정리 먼저 해준다.
        if (clearPoops)
        {
            foreach (var poop in _spawnedPoops)
            {
                objectPool.Return(poop);
            }
        }

        while (true)
        {
            var go = objectPool.Get();
            go.transform.position = new Vector2(Random.Range(-7, 10), 7);

            var poop = go.GetComponent<Poop>();

            _spawnedPoops.Add(go);

            // 속도 조절
            poop.Speed = Random.Range((float)level, (float)level + 1.5f);
            poop.Damage = 1;

            poop.SetObjectPool(objectPool);

            var x = 1.0f / (level + 2);

            var intervalMin = x;
            var intervalMax = x * 5;

            yield return new WaitForSeconds(Random.Range(intervalMin, intervalMax));
        }
    }

    public override void OnEvent(GameEventType eventType, object token)
    {
        if (eventType == GameEventType.GAME_LEVEL_CHANGED)
        {
            StartPoopsRoutine(GameManager.Get().Level, false);
        }
        else if (eventType == GameEventType.GAME_START ||
                 eventType == GameEventType.GAME_STOP)
        {
            StartPoopsRoutine(GameManager.Get().Level, true);
        }
    }
}
