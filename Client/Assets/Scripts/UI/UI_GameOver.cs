using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameOver : MonoBehaviour
{
    public Text txtGameOver;
    public Text txtGameScore;

    public Button btnRestart;

    public void Initialize(int gameScore)
    {
        transform.DOScale(1.0f, 1.5f).From(0.0f).SetEase(Ease.OutBounce).SetAutoKill(true);
        txtGameScore.text = "SCORE: " + gameScore.ToString();
    }

    public void OnRestart()
    {
        EventManager.Get().Dispatch(GameEventType.GAME_RESTART_REQUEST);
        Destroy(this.gameObject);
    }
}
