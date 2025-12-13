using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FilledUp : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private RectTransform _rt;
    public Action<bool> fillTrigger;
    public float duration = 10;
    private bool fillCheck;
    Tween _tween;

    private bool isGameQuit;

    private void Awake()
    {
        _rt = GetComponent<RectTransform>();
    }

    public void FillUp(bool fill)
    {
        if (fillCheck == fill) return;
        if (fill)
        {
            _tween =_rt.DOScaleX(1, duration).SetEase(Ease.Linear)
                .SetUpdate(true)
                .OnComplete(OnFilled);
        }
        else
        {
            _tween = _rt.DOScaleX(0, duration).SetEase(Ease.Linear)
                .SetUpdate(true);
        }
        fillCheck = fill;
    }

    private void OnFilled()
    {
        if (isGameQuit == true)
        {
            Application.Quit();
        }
        else
        {
            Hwan.SceneManager.Instance.OnLoadScene(1);
        }
    }

    public void SetComplete(bool gameQuitStage)
    {
        isGameQuit = gameQuitStage;

        if (gameQuitStage == true)
        {
            text.text = "좌 클릭을 길게 눌러 게임 종료하기";
        }
        else
        {
            text.text = "좌 클릭을 길게 눌러 메인 화면으로";
        }
    }

    public void OnDestroy()
    {
        _tween.Kill();
    }
}