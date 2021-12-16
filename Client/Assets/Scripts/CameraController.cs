using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CameraController : EventBehaviour
{
    public Transform target;

    private Vector2 _shakeValue;

    public void LateUpdate()
    {
        var v = target.position;
        this.transform.position = new Vector3(v.x + _shakeValue.x, v.y + _shakeValue.y, transform.position.z);
    }

    public override void OnEvent(GameEventType eventType, object token)
    {
        if (eventType == GameEventType.PLAYER_ATTACKED)
        {
            DOTween.Shake(() => _shakeValue, (v) => _shakeValue = v, 1.0f, 0.4f);
        }
    }
}
