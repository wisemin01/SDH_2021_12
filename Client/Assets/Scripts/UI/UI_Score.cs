using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Score : EventBehaviour
{
    public Text txtScore;

    public void Start()
    {
        txtScore.text = "SCORE: " + GameManager.Get().Score;
    }

    public override void OnEvent(GameEventType eventType, object token)
    {
        if (eventType == GameEventType.GAME_SCORE_CHANGED)
        {
            var score = (int)token;
            txtScore.text = "SCORE: " + score.ToString();
        }
        else if (eventType == GameEventType.GAME_STOP)
        {
            txtScore.text = string.Empty;
        }
    }
}
