using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_LevelChanged : MonoBehaviour
{
    public Text txtLevel;

    public void Initialize(int level)
    {
        txtLevel.text = "LEVEL " + level.ToString();

        txtLevel.transform.DOScale(1.0f, 2.0f).From(0.0f)
            .SetEase(Ease.OutElastic)
            .OnComplete(() =>
        {
            Destroy(gameObject);
        })
            .SetAutoKill(true);
    }
}
