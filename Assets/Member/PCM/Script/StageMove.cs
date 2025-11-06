using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class StageMove : MonoBehaviour
{
    private int _stage;
    public List<RectTransform> Stags = new List<RectTransform>();
    private RectTransform rect;

    private Coroutine delayCoroutine;
    [SerializeField] private float delayTime = 0.5f;
    public int Stage
    {
        get { return _stage; }
        private set {
            value = Mathf.Clamp(value, 0, Stags.Count - 1);
            if (false == _stage.Equals(value))
            { 
                _stage = value;
                StageValueChanged?.Invoke(_stage);
            }
        }
    }
    public UnityEvent<int> StageValueChanged;
    Vector2 StageScroll;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        StageScroll = Mouse.current.scroll.ReadValue();
        if (StageScroll.y < 0&& delayCoroutine == null)
        {
            int nextStage = Mathf.Clamp(Stage - 1, 0, Stags.Count - 1);
            MoveToStage(nextStage);
            delayCoroutine = StartCoroutine(DelayCoroutine());
        }
        if (StageScroll.y > 0&& delayCoroutine == null)
        {
            int nextStage = Mathf.Clamp(Stage + 1, 0, Stags.Count - 1);
            MoveToStage(nextStage);
            delayCoroutine = StartCoroutine(DelayCoroutine());
        }
    }

    private IEnumerator DelayCoroutine()
    {
        yield return new WaitForSeconds(delayTime);
        delayCoroutine = null;
    }

    private void MoveToStage(int nextStage)
    {
        if (nextStage == Stage) return; // 같은 스테이지면 무시

        float targetPos = -Stags[nextStage].anchoredPosition.x + 70f;
        rect.DOAnchorPosX(targetPos, 1f).SetEase(Ease.OutExpo,3);

        Stage = nextStage;
        Debug.Log($"Stage: {Stage}");
    }
}

