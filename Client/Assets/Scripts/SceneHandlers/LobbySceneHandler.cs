using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbySceneHandler : MonoBehaviour
{
    public Text txtTitle;
    public Text txtTouchToStart;

    public Image imgPoop1;
    public Image imgPoop2;
    public Image imgPlayer;

    private void Start()
    {
        txtTitle.transform.DOScale(1.05f, 2.0f).From(0.9f).SetLoops(-1, LoopType.Yoyo);
        txtTitle.DOColor(Color.red, 2.0f).From(Color.yellow).SetLoops(-1, LoopType.Yoyo);

        txtTouchToStart.transform.DOScale(1.1f, 2.0f).From(1.0f).SetLoops(-1, LoopType.Yoyo);

        imgPoop1.transform.DOJump(imgPoop1.transform.position + new Vector3(0, 50, 0), 3, 1, 2.0f).SetLoops(-1, LoopType.Yoyo);
        imgPoop2.transform.DOScale(1.0f, 2.0f).From(0.9f).SetLoops(-1, LoopType.Yoyo);
        imgPlayer.transform.DOScale(1.0f, 2.0f).From(0.9f).SetLoops(-1, LoopType.Yoyo);
    }

    public void OnStartGame()
    {
        DOTween.KillAll();
        SceneManager.LoadScene("Game");
    }
}
