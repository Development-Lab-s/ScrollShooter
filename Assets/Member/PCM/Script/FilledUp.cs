using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class FilledUp : MonoBehaviour
{
    [SerializeField] private bool isStage;
    private RectTransform _rt;
    public Action<bool> fillTrigger;
    public float duration = 10;
    private bool fillCheck;
    Tween _tween;

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

    }

    public void OnDestroy()
    {
        _tween.Kill();
    }
}